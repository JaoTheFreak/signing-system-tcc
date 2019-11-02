using System;

namespace Signing.System.Tcc.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
