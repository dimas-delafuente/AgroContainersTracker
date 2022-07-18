using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class ExistsRateNameHandler : RatesBaseHandler, IRequestHandler<ExistsRateNameQuery, bool>
    {
        public ExistsRateNameHandler(IRateRepository rateRepository) : base(rateRepository)
        {
        }

        public async Task<bool> Handle(ExistsRateNameQuery request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _rateRepository.ExistsRateNameAsync(request.RateName, cancellationToken).ConfigureAwait(false);
        }
    }
}
