using Microsoft.EntityFrameworkCore;
using Signing.System.Tcc.Domain.RecordAggregate;

namespace Signing.System.Tcc.Data.Repositories
{
    public class RecordRepository : Repository<Record>, IRecordRepository
    {

        public RecordRepository(DbContext context) : base (context)
        {

        }
    }
}
