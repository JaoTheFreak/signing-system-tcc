using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.UserAggregate;
using Signing.System.Tcc.MVC.ViewModels.Account;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAppService _userAppService;

        private readonly IUnitOfWorkAppService _unitOfWorkAppService;

        private readonly IToastNotification _notificationService;

        public AccountController(IUserAppService userAppService, IUnitOfWorkAppService unitOfWorkAppService, IToastNotification toastNotification)
        {
            _userAppService = userAppService;

            _unitOfWorkAppService = unitOfWorkAppService;

            _notificationService = toastNotification;
        }

        [HttpGet, AllowAnonymous]        
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]        
        public IActionResult Login([FromForm] LoginViewModel inputLogin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userAppService.FirstOrDefault(u => u.Email == inputLogin.UserName);

                if (user == null)
                {                    
                    _notificationService.AddErrorToastMessage("Dados incorretos!", new ToastrOptions
                    {
                        CloseButton = true,
                        Title = "Usuário Não Encontrado"                        
                    });

                    return View();
                }

                var passwordToCheck = $"{inputLogin.Password}{user.Salt}";

                if(user.PasswordHash != Helpers.Helpers.GenerateHashSHA256(passwordToCheck))
                {
                    _notificationService.AddErrorToastMessage("Dados incorretos!", new ToastrOptions
                    {
                        CloseButton = true,
                        Title = "Usuário Não Encontrado"
                    });

                    return View();
                }
                
                //LOGAR USUARIO
                
            }

            return View();
        }

        public IActionResult Logout()
        {
            return null;
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

                var newPassword = Helpers.Helpers.GenerateHashSHA256($"{inputRegister.Password}{inputRegister.Salt}");

                var newUser = userFactory.Create(inputRegister.Email, newPassword, inputRegister.Salt, inputRegister.FirstName, inputRegister.LastName);

                _userAppService.Add(newUser);

                if (_unitOfWorkAppService.Complete() == 1)
                {
                    _notificationService.AddErrorToastMessage($"Usuário {inputRegister.Email} registrado com sucesso!", new ToastrOptions
                    {
                        CloseButton = true,
                        Title = "Novo Usuário Registrado"
                    });

                    return RedirectToAction("Login");
                }

                _notificationService.AddErrorToastMessage("Ocorreu um erro ao criar o usuário.", new ToastrOptions
                {
                    CloseButton = true,
                    Title = "Erro ao Criar Usuário"
                });

                return View();
            }

            return View();
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}