using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class CreateStorageHandler : StorageBaseHandler, IRequestHandler<CreateStorageCommand, Storage>
    {
        public CreateStorageHandler(IStorageRepository storageRepository) : base(storageRepository)
        {

        }

        public async Task<Storage> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            var newStorage = await _storageRepository.AddAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
            return newStorage;
        }
    }
}
