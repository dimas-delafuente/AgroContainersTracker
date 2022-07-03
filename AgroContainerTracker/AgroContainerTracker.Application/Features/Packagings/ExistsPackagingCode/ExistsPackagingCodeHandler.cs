using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class ExistsPackagingCodeHandler : PackagingsBaseHandler, IRequestHandler<ExistsPackagingCodeQuery, bool>
    {
        public ExistsPackagingCodeHandler(IPackagingRepository packagingRepository) : base(packagingRepository)
        {
        }

        public async Task<bool> Handle(ExistsPackagingCodeQuery request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _packagingRepository.ExistsPackagingCodeAsync(request.PackagingCode, cancellationToken).ConfigureAwait(false);
        }
    }
}
