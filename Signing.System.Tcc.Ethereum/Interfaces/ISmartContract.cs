using System;
using System.Threading.Tasks;

namespace Signing.System.Tcc.Ethereum.Interfaces
{
    public interface ISmartContract
    {
        Task<(string AuthorDocument, string ImageHash, DateTime CreatedAt)> VerifyImageByHashAsync(string hashImage);

        Task RegisterImageAsync(string authorDocument, string hashImage);

        Task VerifyImageByAuthorDocumentAsync(string authorDocument);
    }
}
