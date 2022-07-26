﻿namespace AgroContainerTracker.Domain
{
    public interface IFruitRepository : IRepository<Fruit>
    {
        Task<IEnumerable<Fruit>> GetAllAsync(CancellationToken cancellationToken);
        Task<Fruit> GetByIdAsync(int fruitId, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int fruitId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Fruit fruit, CancellationToken cancellationToken);
        Task<Fruit> AddAsync(Fruit fruit, CancellationToken cancellationToken);
    }
}