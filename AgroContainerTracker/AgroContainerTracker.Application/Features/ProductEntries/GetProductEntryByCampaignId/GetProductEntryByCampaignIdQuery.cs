using System.Collections.Generic;
using AgroContainerTracker.Domain;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class GetInputByCampaignIdQuery : IRequest<IEnumerable<Input>>
    {
        public int CampaignId { get; set; }

        public GetInputByCampaignIdQuery(int campaignId)
        {
            CampaignId = campaignId;
        }
    }
}