using Signing.System.Tcc.Domain.EtherAggregate;
using System;
using System.Threading.Tasks;

namespace Signing.System.Tcc.Ethereum.Interfaces
{
    public interface ISmartContract
    {
        Task<(string AuthorDocument, string ImageHash, DateTime CreatedAt)> VerifyImageByHashAsync(string hashImage);

        Task<decimal> EstimateTransactionPriceAsync(string authorDocument, string hashImage, EtherValueObject ether);

        Task<(string txHash, decimal txFee, bool txSuccess)> RegisterImageAsync(string authorDocument, string hashImage, EtherValueObject ether);

        Task VerifyImageByAuthorDocumentAsync(string authorDocument);
    }
}
