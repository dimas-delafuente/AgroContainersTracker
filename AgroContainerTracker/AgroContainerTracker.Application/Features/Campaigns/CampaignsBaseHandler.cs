using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Application.Features
{
    internal abstract class CampaignBaseHandler
    {
        protected readonly ICampaignRepository _campaignRepository;

        protected CampaignBaseHandler(ICampaignRepository campaignRepository)
        {
            Ensure.NotNull(campaignRepository, nameof(campaignRepository));
            _campaignRepository = campaignRepository;
        }
    }
}
