using Signing.System.Tcc.Domain.UserAggregate;
using System;

namespace Signing.System.Tcc.Domain.RecordAggregate
{
    public class Record
    {
        public Record(string transactionHash, decimal transactionFee, string midiaHash, string midiaDescription,
            string midiaName, string midiaResolution, string midiaExtension, int midiaSizeBytes, string midiaUrl)
        {
            TransactionHash = transactionHash;

            TransactionFee = transactionFee;

            MidiaHash = midiaHash;

            MidiaDescription = midiaDescription;

            MidiaName = midiaName;

            MidiaExtension = midiaExtension;

            MidiaSizeBytes = midiaSizeBytes;

            MidiaResolution = midiaResolution;

            MidiaUrl = midiaUrl;

            CreatedAt = DateTime.Now;
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string TransactionHash { get; private set; }
        public decimal TransactionFee { get; private set; }
        public string MidiaHash { get; private set; }
        public string MidiaDescription { get; private set; }
        public string MidiaName { get; private set; }
        public string MidiaExtension { get; private set; }
        public string MidiaResolution { get; private set; }
        public int MidiaSizeBytes { get; private set; }
        public string MidiaUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAte { get; private set; }

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                if (_user == null)
                {
                    _user = value;
                    UserId = _user.Id;
                }
            }
        }
    }
}
