using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Signing.System.Tcc.MVC.ViewModels.Account
{
    public class LoginViewModel
    {        
        [DisplayName("Usuário"), Required(ErrorMessage = "O Usuário precisa ser informado!")]
        public string UserName { get; set; }
        [DisplayName("Senha"), Required(ErrorMessage = "A Senha precisa ser informada!")]
        public string Password { get; set; }
    }
}
