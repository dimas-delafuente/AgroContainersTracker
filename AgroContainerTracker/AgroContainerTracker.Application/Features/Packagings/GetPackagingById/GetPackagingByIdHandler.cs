using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetPackagingByIdHandler : PackagingsBaseHandler, IRequestHandler<GetPackagingByIdQuery, Packaging>
    {
        public GetPackagingByIdHandler(IPackagingRepository packagingRepository) : base(packagingRepository)
        {
        }

        public async Task<Packaging> Handle(GetPackagingByIdQuery request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _packagingRepository.GetByIdAsync(request.PackagingId, cancellationToken).ConfigureAwait(false);
        }
    }
}
