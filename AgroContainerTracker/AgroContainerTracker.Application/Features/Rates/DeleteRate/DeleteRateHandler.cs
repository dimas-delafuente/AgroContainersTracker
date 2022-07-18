using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class DeleteRateHandler : RatesBaseHandler, IRequestHandler<DeleteRateCommand, bool>
    {
        public DeleteRateHandler(IRateRepository rateRepository) : base(rateRepository)
        {
        }

        public async Task<bool> Handle(DeleteRateCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _rateRepository.DeleteAsync(request.RateId, cancellationToken).ConfigureAwait(false);
        }
    }
}
