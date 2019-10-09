using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Signing.System.Tcc.MVC.ViewModels.Account
{
    public class LoginViewModel
    {        
        [DisplayName("Usuário")]
        [Required(ErrorMessage = "O campo {0} precisa ser informada e não pode estar em branco!", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo {0} precisa ser informada e não pode estar em branco!", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
