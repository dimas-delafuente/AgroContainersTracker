using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class UpdateStorageHandler : StorageBaseHandler, IRequestHandler<UpdateStorageCommand, bool>
    {
        public UpdateStorageHandler(IStorageRepository storageRepository) : base(storageRepository)
        {
        }

        public async Task<bool> Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            Ensure.Positive(request.StorageId, nameof(request.StorageId));

            return await _storageRepository.UpdateAsync(request.StorageId, request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
