using AgroContainerTracker.Domain.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services
{
    public interface IProductWeighingService
    {
        Task<List<ProductWeighing>> GetAllAsync(int campaingId, int entryNumber);
        Task<ProductWeighing> AddAsync(AddProductWeighingRequest productWeighing);
        Task<ProductWeighing> GetByIdAsync(int productWeighingId);
    }
}
