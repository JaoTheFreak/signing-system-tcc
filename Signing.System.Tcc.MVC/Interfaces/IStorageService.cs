using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Signing.System.Tcc.MVC.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadImageAsync(IFormFile image, string id);
    }
}
