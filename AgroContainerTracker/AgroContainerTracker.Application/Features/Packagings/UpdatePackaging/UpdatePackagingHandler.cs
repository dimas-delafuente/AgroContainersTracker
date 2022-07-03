using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class UpdatePackagingHandler : PackagingsBaseHandler, IRequestHandler<UpdatePackagingCommand, bool>
    {
        public UpdatePackagingHandler(IPackagingRepository packagingRepository) : base(packagingRepository)
        {
        }

        public async Task<bool> Handle(UpdatePackagingCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _packagingRepository.UpdateAsync(request.PackagingId, request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
