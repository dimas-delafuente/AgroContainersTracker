using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class ExistsInputQuery : IRequest<bool>
    {
        public int CampaignId { get; set; }
        public int InputNumber { get; set; }

        public ExistsInputQuery(int campaignId, int InputNumber)
        {
            CampaignId = campaignId;
            InputNumber = InputNumber;
        }
    }
}