using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Application.Features
{
    internal abstract class PackagingsBaseHandler
    {
        protected readonly IPackagingRepository _packagingRepository;
        public PackagingsBaseHandler(IPackagingRepository packagingRepository)
        {
            Ensure.NotNull(packagingRepository, nameof(packagingRepository));
            _packagingRepository = packagingRepository;
        }
    }
}
