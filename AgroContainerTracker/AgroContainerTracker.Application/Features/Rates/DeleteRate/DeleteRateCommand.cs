using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class DeleteRateCommand : IRequest<bool>
    {
        public int RateId { get; set; }
        public DeleteRateCommand(int rateId)
        {
            Ensure.Positive(rateId, nameof(rateId));
            RateId = rateId;
        }
    }
}
