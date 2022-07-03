using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class CreatePackagingHandler : PackagingsBaseHandler, IRequestHandler<CreatePackagingMovementCommand, Packaging>
    {
        public CreatePackagingHandler(IPackagingRepository packagingRepository) : base(packagingRepository)
        {
        }

        public async Task<Packaging> Handle(CreatePackagingMovementCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _packagingRepository.AddAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
