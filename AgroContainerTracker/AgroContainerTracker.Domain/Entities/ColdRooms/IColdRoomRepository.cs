using AgroContainerTracker.Domain.Common;

namespace AgroContainerTracker.Domain.Entities
{
    public interface IColdRoomRepository : IRepository<Country>
    {
        Task<IEnumerable<ColdRoom>> GetAllAsync(CancellationToken cancellationToken);
        Task<ColdRoom> GetByIdAsync(int coldRoomId, CancellationToken cancellationToken);
        Task<bool> ExistsColdRoomNumberAsync(int coldRoomNumber, CancellationToken cancellationToken);
        Task<ColdRoom> AddAsync(ColdRoom coldRoom, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int coldRoomId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(int coldRoomId, ColdRoom coldRoom, CancellationToken cancellationToken);
    }
}
