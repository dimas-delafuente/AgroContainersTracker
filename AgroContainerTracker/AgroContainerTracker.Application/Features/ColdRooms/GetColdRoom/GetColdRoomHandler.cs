using AgroContainerTracker.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetStorageHandler : StorageBaseHandler, IRequestHandler<GetStorageQuery, Storage>
    {
        public GetStorageHandler(IStorageRepository storageRepository) : base(storageRepository)
        {

        }

        public async Task<Storage> Handle(GetStorageQuery request, CancellationToken cancellationToken)
        {
            return await _storageRepository.GetByIdAsync(request.StorageId, cancellationToken).ConfigureAwait(false);
        }
    }
}
