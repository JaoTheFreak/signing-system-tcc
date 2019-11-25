using Signing.System.Tcc.Domain.Service;

namespace Signing.System.Tcc.Domain.RecordAggregate
{
    public class RecordService : Service<Record>, IRecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepository) : base(recordRepository)     
        {
            _recordRepository = recordRepository;
        }
    }
}
