using AgroContainerTracker.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class ExistsStorageHandler : StorageBaseHandler, IRequestHandler<ExistsStorageNumberQuery, bool>
    {
        public ExistsStorageHandler(IStorageRepository storageRepository) : base(storageRepository)
        {

        }

        public async Task<bool> Handle(ExistsStorageNumberQuery request, CancellationToken cancellationToken)
        {
            return await _storageRepository.ExistsByStorageNumber(request.StorageNumber, cancellationToken).ConfigureAwait(false);
        }
    }
}
