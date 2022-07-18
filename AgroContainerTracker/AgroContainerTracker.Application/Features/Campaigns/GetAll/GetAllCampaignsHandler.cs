using AgroContainerTracker.Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetAllCampaignHandler : CampaignBaseHandler, IRequestHandler<GetAllCampaignsQuery, IEnumerable<Campaign>>
    {
        public GetAllCampaignHandler(ICampaignRepository campaignRepository) : base(campaignRepository)
        {

        }

        public async Task<IEnumerable<Campaign>> Handle(GetAllCampaignsQuery request, CancellationToken cancellationToken)
        {
            return await _campaignRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
