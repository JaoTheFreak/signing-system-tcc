using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.UserAggregate;
using Signing.System.Tcc.MVC.Interfaces;
using Signing.System.Tcc.MVC.ViewModels.Account;
using System.Threading.Tasks;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAppService _userAppService;

        private readonly IUnitOfWorkAppService _unitOfWorkAppService;

        private readonly IToastNotification _notificationService;

        private readonly IAuthenticantionService _authorizationService;

        public AccountController(IUserAppService userAppService, IUnitOfWorkAppService unitOfWorkAppService, IToastNotification toastNotification, IAuthenticantionService authorizationService)
        {
            _userAppService = userAppService;

            _unitOfWorkAppService = unitOfWorkAppService;

            _notificationService = toastNotification;

            _authorizationService = authorizationService;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!User.Identity.IsAuthenticated)
                return View();

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel inputLogin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userAppService.FirstOrDefault(u => u.Email == inputLogin.UserName && u.PasswordHash == Helpers.Helpers.GenerateHashSHA256($"{inputLogin.Password}{u.Salt}"));

                if (user != null)
                {
                    await _authorizationService.SignInAsync(user, HttpContext);

                    _notificationService.AddSuccessToastMessage($"Bem Vindo {user.DisplayName}!", new ToastrOptions
                    {
                        Title = "Login Efetuado",
                        CloseButton = true
                    });

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Document");
                }

                _notificationService.AddErrorToastMessage("Dados incorretos!", new ToastrOptions
                {
                    CloseButton = true,
                    Title = "Usuário Não Encontrado"
                });

                return View();
            }

            return View();
        }

        [HttpPost, ActionName("Logout"), ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutPost()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _authorizationService.SignOutAsync(HttpContext);
            }

            _notificationService.AddSuccessToastMessage($"Volte Sempre {User.Identity.Name}!", new ToastrOptions
            {
                CloseButton = true,
                Title = "Até breve"
            });

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Document", "Index");

            return RedirectToAction("Login");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public IActionResult Register([FromForm] RegisterViewModel inputRegister, [FromServices] IUserFactory userFactory)
        {
            if (ModelState.IsValid)
            {
                var user = _userAppService.FirstOrDefault(u => u.Email == inputRegister.Email);

                if (user != null)
                {
                    _notificationService.AddErrorToastMessage("Já existe um usuário com o e-mail cadastrado!", new ToastrOptions
                    {
                        CloseButton = true,
                        Title = "Usuario Existente"
                    });

                    return View();
                }

                user = _userAppService.FirstOrDefault(u => u.DocumentNumber.Equals(inputRegister.DocumentNumber));

                if (user != null)
                {
                    _notificationService.AddErrorToastMessage("Já existe um usuário com o documento cadastrado!", new ToastrOptions
                    {
                        CloseButton = true,
                        Title = "Usuario Existente"
                    });

                    return View();
                }

                var newPassword = Helpers.Helpers.GenerateHashSHA256($"{inputRegister.Password}{inputRegister.Salt}");

                var newUser = userFactory.Create(inputRegister.Email, newPassword, inputRegister.Salt, inputRegister.FirstName, inputRegister.LastName, inputRegister.DocumentNumber);

                _userAppService.Add(newUser);

                if (_unitOfWorkAppService.Complete() == 1)
                {
                    _notificationService.AddSuccessToastMessage($"Usuário {inputRegister.Email} registrado com sucesso!", new ToastrOptions
                    {
                        CloseButton = true,
                        Title = "Novo Usuário Registrado"
                    });

                    return RedirectToAction("Login");
                }

                return RedirectToAction("Login");
            }

            _notificationService.AddErrorToastMessage("Ocorreu um erro ao criar o usuário.", new ToastrOptions
            {
                CloseButton = true,
                Title = "Erro ao Criar Usuário"
            });

            return View();
        }    

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            _notificationService.AddErrorToastMessage("Não autorizado!", new ToastrOptions { Title = "Erro", CloseButton = true });

            return RedirectToAction("DashBoard", "Home");
        }
    }
}