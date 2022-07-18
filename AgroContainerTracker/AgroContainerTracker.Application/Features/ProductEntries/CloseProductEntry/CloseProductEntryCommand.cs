using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class CloseInputCommand : IRequest<bool>
    {
        public int CampaignId { get; set; }
        public int InputNumber { get; set; }

        public CloseInputCommand(int campaignId, int InputNumber)
        {
            CampaignId = campaignId;
            InputNumber = InputNumber;
        }
    }
}