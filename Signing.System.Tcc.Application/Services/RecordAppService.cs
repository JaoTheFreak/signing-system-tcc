using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.RecordAggregate;

namespace Signing.System.Tcc.Application.Services
{
    public class RecordAppService : AppService<Record>, IRecordAppService
    {
        private readonly IRecordService _recordService;

        public RecordAppService(IRecordService recordService) : base(recordService)
        {
            _recordService = recordService;
        }
    }
}
