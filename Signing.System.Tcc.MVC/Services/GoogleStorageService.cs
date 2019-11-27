using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Signing.System.Tcc.MVC.Interfaces;
using System.Threading.Tasks;

namespace Signing.System.Tcc.MVC.Services
{
    public class GoogleStorageService : IStorageService
    {
        private readonly StorageClient _storageClient;

        private readonly string _bucketName;

        public GoogleStorageService(string bucketName)
        {            
            _storageClient = StorageClient.Create();

            _bucketName = bucketName;
        }

        public async Task<string> UploadImageAsync(IFormFile image, string id)
        {
            var imageAcl = PredefinedObjectAcl.PublicRead;

            var imageObject = await _storageClient.UploadObjectAsync(
                bucket: _bucketName,
                objectName: id,
                contentType: image.ContentType,
                source: image.OpenReadStream(),
                options: new UploadObjectOptions { PredefinedAcl = imageAcl }
            );

            return imageObject.MediaLink;
        } 

    }
}
