using Signing.System.Tcc.Domain.UserAggregate;

namespace Signing.System.Tcc.Domain.RecordAggregate
{
    public class RecordFactory : IRecordFactory
    {
        public Record Create(string transactionHash, decimal transactionFee, string midiaHash, string midiaDescription, string midiaName, string midiaResolution, string midiaExtension, long midiaSizeBytes, string midiaUrl)
        {
            return new Record(transactionHash, transactionFee, midiaHash, midiaDescription, midiaName, midiaResolution, midiaExtension, midiaSizeBytes, midiaUrl);
        }
    }
}
