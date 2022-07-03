using AgroContainerTracker.Domain.Common;

namespace AgroContainerTracker.Domain.Entities
{
    public interface IPackagingRepository : IRepository<Packaging>
    {
        public Task<IEnumerable<Packaging>> GetAllAsync(CancellationToken cancellationToken);
        Task<Packaging> GetByIdAsync(int packagingId, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int packagingId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(int packagingId, Packaging packaging, CancellationToken cancellationToken);
        Task<Packaging> AddAsync(Packaging packaging, CancellationToken cancellationToken);
        Task<bool> ExistsPackagingCodeAsync(string packagingCode, CancellationToken cancellationToken);
    }
}
