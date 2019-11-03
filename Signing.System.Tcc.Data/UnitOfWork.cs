using Microsoft.EntityFrameworkCore;
using Signing.System.Tcc.Domain.UnitOfWork;

namespace Signing.System.Tcc.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(DbContext sigurContext)
        {
            context = sigurContext;
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
