using AgroContainerTracker.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class DeleteStorageHandler : StorageBaseHandler, IRequestHandler<DeleteStorageCommand, bool>
    {
        public DeleteStorageHandler(IStorageRepository storageRepository) : base(storageRepository)
        {

        }

        public async Task<bool> Handle(DeleteStorageCommand request, CancellationToken cancellationToken)
        {
            return await _storageRepository.DeleteAsync(request.StorageId, cancellationToken).ConfigureAwait(false);
        }
    }
}
