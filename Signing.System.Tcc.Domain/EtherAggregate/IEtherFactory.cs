using System.Threading.Tasks;

namespace Signing.System.Tcc.Domain.EtherAggregate
{
    public interface IEtherFactory
    {
        Task<EtherValueObject> CreateEtherAsync();
    }
}
