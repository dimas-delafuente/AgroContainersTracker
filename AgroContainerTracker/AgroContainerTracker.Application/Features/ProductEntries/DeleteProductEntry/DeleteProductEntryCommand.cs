using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class DeleteInputCommand : IRequest<bool>
    {
        public int CampaignId { get; set; }
        public int InputNumber { get; set; }

        public DeleteInputCommand(int campaignId, int InputNumber)
        {
            CampaignId = campaignId;
            InputNumber = InputNumber;
        }
    }
}