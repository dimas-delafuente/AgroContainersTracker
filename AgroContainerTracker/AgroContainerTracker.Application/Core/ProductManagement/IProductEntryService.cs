using AgroContainerTracker.Domain.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services
{
    public interface IProductEntryService
    {
        Task<List<ProductEntry>> GetByCampaingIdAsync(int campaingId);
        Task<ProductEntry> AddAsync(ProductEntry productEntry);
        Task<ProductEntry> GetAsync(int campaingId, int productEntryNumber);
        Task<bool> ExistsAsync(int campaingId, int productEntryNumber);
        Task<bool> UpdateAsync(ProductEntry productEntry);
        Task<bool> DeleteAsync(int campaingId, int productEntryNumber);
    }
}
