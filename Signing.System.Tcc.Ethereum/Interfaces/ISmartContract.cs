using System.Threading.Tasks;

namespace Signing.System.Tcc.Ethereum.Interfaces
{
    public interface ISmartContract
    {
        Task<string> VerifyImageByHashAsync(string hashImage);
    }
}
