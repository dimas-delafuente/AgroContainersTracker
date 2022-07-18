using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services
{
    public interface ICampaignService
    {
        Task<int> GetCampaignNextEntryId(int CampaignId);
        Task<int> GetCampaignNextWeightingId(int CampaignId);
    }
}
