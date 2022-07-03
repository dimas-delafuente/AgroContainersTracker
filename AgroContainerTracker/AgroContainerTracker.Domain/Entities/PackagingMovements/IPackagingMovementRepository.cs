using AgroContainerTracker.Domain.Common;

namespace AgroContainerTracker.Domain.Entities
{
    public interface IPackagingMovementRepository : IRepository<PackagingMovement>
    {
        Task<PackagingMovement> AddAsync(PackagingMovement packagingMovement, CancellationToken cancellationToken);
        Task<IEnumerable<PackagingMovement>> GetByCustomerIdAsync(int customerId, DateTime initDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
