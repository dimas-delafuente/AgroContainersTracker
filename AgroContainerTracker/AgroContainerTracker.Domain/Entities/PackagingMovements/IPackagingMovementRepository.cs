using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Domain
{
    public interface IPackagingMovementRepository : IRepository<PackagingMovement>
    {
        Task<PackagingMovement> AddAsync(PackagingMovement packagingMovement, CancellationToken cancellationToken);
        Task<IEnumerable<PackagingMovement>> GetByCustomerIdAsync(int customerId, DateTime initDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
