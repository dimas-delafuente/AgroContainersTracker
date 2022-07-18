using AgroContainerTracker.Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetAllPackagingsHandler : PackagingsBaseHandler, IRequestHandler<GetAllPackagingsQuery, IEnumerable<Packaging>>
    {
        public GetAllPackagingsHandler(IPackagingRepository packagingRepository) : base(packagingRepository)
        {
        }

        public async Task<IEnumerable<Packaging>> Handle(GetAllPackagingsQuery request, CancellationToken cancellationToken)
        {
            return await _packagingRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
