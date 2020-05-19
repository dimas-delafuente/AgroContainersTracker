using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Fruits;

namespace AgroContainerTracker.Core.Services
{
    public interface IFruitService
    {
        Task<List<Fruit>> GetAllAsync();
        Task<Fruit> GetByIdAsync(int fruitId);
        Task<bool> DeleteAsync(int fruitId);
        Task<bool> UpdateAsync(Fruit fruit);
        Task<Fruit> AddAsync(AddFruitRequest fruit);

    }
}
