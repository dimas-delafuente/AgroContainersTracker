using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class GetRateByIdQuery : IRequest<Rate>
    {
        public int RateId { get; set; }
        public GetRateByIdQuery(int rateId)
        {
            Ensure.Positive(rateId, nameof(rateId));
            RateId = rateId;
        }
    }
}
