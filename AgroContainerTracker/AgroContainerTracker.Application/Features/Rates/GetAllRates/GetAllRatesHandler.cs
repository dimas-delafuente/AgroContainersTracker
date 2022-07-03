using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetAllRatesHandler : RatesBaseHandler, IRequestHandler<GetAllRatesQuery, IEnumerable<Rate>>
    {
        public GetAllRatesHandler(IRateRepository rateRepository) : base(rateRepository)
        {
        }

        public async Task<IEnumerable<Rate>> Handle(GetAllRatesQuery request, CancellationToken cancellationToken)
        {
            return await _rateRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
