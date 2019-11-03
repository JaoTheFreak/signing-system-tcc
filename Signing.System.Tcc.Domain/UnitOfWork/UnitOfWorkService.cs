namespace Signing.System.Tcc.Domain.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int Complete()
        {
            return unitOfWork.Complete();
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }
    }
}
