using AgroContainerTracker.Domain.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services
{
    public interface ICampaingService
    {
        Task<IEnumerable<Campaing>> GetAllAsync();
        Task<int> GetCampaingNextEntryId(int campaingId);
    }
}
