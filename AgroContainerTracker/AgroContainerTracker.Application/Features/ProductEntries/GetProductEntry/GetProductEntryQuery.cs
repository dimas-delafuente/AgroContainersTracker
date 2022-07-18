using AgroContainerTracker.Domain;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class GetInputQuery : IRequest<Input>
    {
        public int CampaignId { get; set; }
        public int InputNumber { get; set; }

        public GetInputQuery(int campaignId, int InputNumber)
        {
            CampaignId = campaignId;
            InputNumber = InputNumber;
        }
    }
}