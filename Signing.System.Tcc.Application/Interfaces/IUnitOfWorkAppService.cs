using System;

namespace Signing.System.Tcc.Application.Interfaces
{
    public interface IUnitOfWorkAppService : IDisposable
    {
        int Complete();
    }
}
