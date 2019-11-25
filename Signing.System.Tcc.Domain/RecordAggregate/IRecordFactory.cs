using Signing.System.Tcc.Domain.UserAggregate;

namespace Signing.System.Tcc.Domain.RecordAggregate
{
    public interface IRecordFactory
    {
        Record Create(User recordOwner, string transactionHash, decimal transactionFee, string midiaHash, string midiaDescription,
            string midiaName, string midiaResolution, string midiaExtension, int midiaSizeByte, string midiaUrl);

    }
}
