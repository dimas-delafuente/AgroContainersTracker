using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class UpdateRateHandler : RatesBaseHandler, IRequestHandler<UpdateRateCommand, bool>
    {
        public UpdateRateHandler(IRateRepository rateRepository) : base(rateRepository)
        {
        }

        public async Task<bool> Handle(UpdateRateCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            Ensure.Positive(request.RateId, nameof(request.RateId));
            return await _rateRepository.UpdateAsync(request.RateId, request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
