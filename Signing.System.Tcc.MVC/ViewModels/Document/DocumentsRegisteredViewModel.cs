using System;

namespace Signing.System.Tcc.MVC.ViewModels.Document
{
    public class DocumentsRegisteredViewModel
    {
        public string ImageUrl { get; set; }
        public string ArtName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool RegisterSuccessTrue { get; set; }
        public string ImageHash { get; set; }

    }
}
