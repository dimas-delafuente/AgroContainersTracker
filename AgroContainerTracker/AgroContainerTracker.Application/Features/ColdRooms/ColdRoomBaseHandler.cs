using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Application.Features
{
    internal abstract class StorageBaseHandler
    {
        protected readonly IStorageRepository _storageRepository;

        public StorageBaseHandler(IStorageRepository storageRepository)
        {
            Ensure.NotNull(storageRepository, nameof(storageRepository));
            _storageRepository = storageRepository;
        }
    }
}
