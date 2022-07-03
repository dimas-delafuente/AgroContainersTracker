using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class DeletePackagingHandler : PackagingsBaseHandler, IRequestHandler<DeletePackagingCommand, bool>
    {
        public DeletePackagingHandler(IPackagingRepository packagingRepository) : base(packagingRepository)
        {
        }

        public async Task<bool> Handle(DeletePackagingCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _packagingRepository.DeleteAsync(request.PackagingId, cancellationToken).ConfigureAwait(false);
        }
    }
}
