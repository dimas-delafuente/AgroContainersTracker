using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features.Packagings.GetPackagingById
{
    internal class GetCustomerPackagingsHandler : PackagingMovementsBaseHandler, IRequestHandler<GetCustomerPackagingsQuery, IEnumerable<PackagingMovement>>
    {
        public GetCustomerPackagingsHandler(IPackagingMovementRepository packagingMovementRepository) : base(packagingMovementRepository)
        {
        }

        public async Task<IEnumerable<PackagingMovement>> Handle(GetCustomerPackagingsQuery request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _packagingMovementRepository.GetByCustomerIdAsync(request.CustomerId, request.InitDate, request.EndDate, cancellationToken).ConfigureAwait(false);
        }
    }
}
