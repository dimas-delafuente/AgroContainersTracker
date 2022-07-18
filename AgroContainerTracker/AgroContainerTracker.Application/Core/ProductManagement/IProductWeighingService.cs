using AgroContainerTracker.Domain.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services
{
    public interface IWeighingService
    {
        Task<List<Weighing>> GetAllAsync(int CampaignId, int entryNumber);
        Task<Weighing> AddAsync(AddWeighingRequest Weighing);
        Task<Weighing> UpdateAsync(AddWeighingRequest Weighing);
        Task<Weighing> GetByIdAsync(int WeighingId);
        Task<bool> DeleteAsync(int WeighingId);
    }
}
