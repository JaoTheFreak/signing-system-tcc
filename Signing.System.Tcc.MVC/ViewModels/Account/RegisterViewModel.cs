using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Signing.System.Tcc.MVC.ViewModels.Account
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Salt = Helpers.Helpers.GenerateHashSHA256($"{Guid.NewGuid().ToString()}{DateTime.Now.ToString()}");
        } 

        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Nome")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Sobrenome")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "O campo {0} não contém um endereço de e-mail válido!")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório! E não pode estar em branco!", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Password
        {
            get => Helpers.Helpers.GenerateHashSHA256(_realPassword);

            set
            {
                _realPassword = value;
            }
        }

        [Required(ErrorMessage = "O campo {0} é obrigatório! E não pode estar em branco!", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DisplayName("Confirmação de Senha")]
        [Compare("Password", ErrorMessage = "Senhas diferentes, favor verificar!")]
        [DataType(DataType.Password)]
        public string CheckPassword
        {
            get => Helpers.Helpers.GenerateHashSHA256(_realPassword);

            set
            {
                _realCheckPassword = value;
            }
        }

        public string Salt { get; }

        private string _realPassword;

        private string _realCheckPassword;
    }
}
