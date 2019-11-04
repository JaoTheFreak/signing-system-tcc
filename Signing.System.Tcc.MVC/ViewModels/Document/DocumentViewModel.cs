using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Signing.System.Tcc.MVC.ViewModels.Document
{
    public class DocumentViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Nome do autor")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string AuthorImageName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Número do documento do autor")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string AuthoDocumentId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Tipo do documento do autor")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string AuthoDocumentType { get; set; }

        [DisplayName("Chave da transação")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string TransactionId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Hash da imagem")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string ImageHash { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Data do registro")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Date)]
        public DateTime ImageRegisterDate { get; private set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DisplayName("Nome da obra")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string DocumentName { get; set; }

        [DisplayName("Formato da imagem")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string DocumentFormat { get; set; }

        [DisplayName("Tamanho da imagem")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string DocumentSize { get; set; }

        [DisplayName("Descrição da image")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [DataType(DataType.Text)]
        public string DocumentDescription { get; set; }

        [DisplayName("Imagem")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!", AllowEmptyStrings = false)]
        [DataType(DataType.Upload)]
        public IFormFile ImagePath { get; set; }

    }
}
