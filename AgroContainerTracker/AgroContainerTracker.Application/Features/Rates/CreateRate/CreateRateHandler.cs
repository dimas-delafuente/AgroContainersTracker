using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class CreateRateHandler : RatesBaseHandler, IRequestHandler<CreateRateCommand, Rate>
    {
        public CreateRateHandler(IRateRepository rateRepository) : base(rateRepository)
        {
        }

        public async Task<Rate> Handle(CreateRateCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _rateRepository.AddAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
