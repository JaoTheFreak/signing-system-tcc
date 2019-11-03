using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.UnitOfWork;

namespace Signing.System.Tcc.Application.Services
{
    public class UnitOfWorkAppService : IUnitOfWorkAppService
    {
        private readonly IUnitOfWorkService unitOfWorkService;

        public UnitOfWorkAppService(IUnitOfWorkService unitOfWorkService)
        {
            this.unitOfWorkService = unitOfWorkService;
        }

        public int Complete()
        {
            return unitOfWorkService.Complete();
        }

        public void Dispose()
        {
            unitOfWorkService?.Dispose();
        }
    }
}
