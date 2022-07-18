using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Domain
{
    public interface IStorageRepository : IRepository<Storage>
    {
        Task<IEnumerable<Storage>> GetAllAsync(CancellationToken cancellationToken);
        Task<Storage> GetByIdAsync(int storageId, CancellationToken cancellationToken);
        Task<bool> ExistsByStorageNumber(int storageNumber, CancellationToken cancellationToken);
        Task<Storage> AddAsync(Storage storage, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int storageId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(int storageId, Storage storage, CancellationToken cancellationToken);
    }
}
