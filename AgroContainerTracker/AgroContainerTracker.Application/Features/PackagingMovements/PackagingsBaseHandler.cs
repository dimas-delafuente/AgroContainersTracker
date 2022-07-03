using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Application.Features
{
    internal abstract class PackagingMovementsBaseHandler
    {
        protected readonly IPackagingMovementRepository _packagingMovementRepository;
        public PackagingMovementsBaseHandler(IPackagingMovementRepository packagingMovementRepository)
        {
            Ensure.NotNull(packagingMovementRepository, nameof(packagingMovementRepository));
            _packagingMovementRepository = packagingMovementRepository;
        }
    }
}
