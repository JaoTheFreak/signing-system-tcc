using System;

namespace Signing.System.Tcc.Domain.UnitOfWork
{
    public interface IUnitOfWorkService : IDisposable
    {
        int Complete();
    }
}
