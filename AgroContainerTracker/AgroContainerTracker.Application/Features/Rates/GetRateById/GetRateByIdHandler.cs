using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetRateByIdHandler : RatesBaseHandler, IRequestHandler<GetRateByIdQuery, Rate>
    {
        public GetRateByIdHandler(IRateRepository rateRepository) : base(rateRepository)
        {
        }

        public async Task<Rate> Handle(GetRateByIdQuery request, CancellationToken cancellationToken)
        {
            return await _rateRepository.GetByIdAsync(request.RateId, cancellationToken).ConfigureAwait(false);
        }
    }
}
