﻿using Signing.System.Tcc.Domain.UserAggregate;

namespace Signing.System.Tcc.Domain.RecordAggregate
{
    public class RecordFactory : IRecordFactory
    {
        public Record Create(User recordOwner, string transactionHash, decimal transactionFee, string midiaHash, string midiaDescription, string midiaName, string midiaResolution, string midiaExtension, int midiaSizeByte, string midiaUrl)
        {
            return new Record(recordOwner, transactionHash, transactionFee, midiaHash, midiaDescription, midiaName, midiaResolution, midiaExtension, midiaSizeByte, midiaUrl);
        }
    }
}
