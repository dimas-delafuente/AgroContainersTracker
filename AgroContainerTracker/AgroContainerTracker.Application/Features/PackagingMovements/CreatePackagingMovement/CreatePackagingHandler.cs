using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class CreatePackagingMovementHandler : PackagingMovementsBaseHandler, IRequestHandler<CreatePackagingMovementCommand, PackagingMovement>
    {
        public CreatePackagingMovementHandler(IPackagingMovementRepository packagingMovementRepository) : base(packagingMovementRepository)
        {
        }

        public async Task<PackagingMovement> Handle(CreatePackagingMovementCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _packagingMovementRepository.AddAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
