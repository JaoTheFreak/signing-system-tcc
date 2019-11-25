using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Signing.System.Tcc.MVC.ViewModels.Document
{
    public class DocumentRegisterViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Nome da obra")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string DocumentName { get; set; }
        
        [DisplayName("Descrição da imagem")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string DocumentDescription { get; set; }

        [DisplayName("Hash da imagem")]
        [Required]
        [DataType(DataType.Text)]
        public string ImageHash { get; set; }

        [DisplayName("Imagem")]
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

    }
}
