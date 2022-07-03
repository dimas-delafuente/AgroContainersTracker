using AgroContainerTracker.Domain.Common;

namespace AgroContainerTracker.Domain.Entities
{
    public interface IFruitRepository : IRepository<Fruit>
    {
        Task<IEnumerable<Fruit>> GetAllAsync(CancellationToken cancellationToken);
        Task<Fruit> GetByIdAsync(int fruitId, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int fruitId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(int fruitId, Fruit fruit, CancellationToken cancellationToken);
        Task<Fruit> AddAsync(Fruit fruit, CancellationToken cancellationToken);
    }
}
