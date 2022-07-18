using System.Threading;
using System.Threading.Tasks;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    internal class ExistsInputHandler : InputBaseHandler, IRequestHandler<ExistsInputQuery, bool>
    {
        public ExistsInputHandler(IInputRepository InputRepository) : base(InputRepository)
        {
        }

        public async Task<bool> Handle(ExistsInputQuery request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _InputRepository.ExistsAsync(request.CampaignId, request.InputNumber, cancellationToken).ConfigureAwait(false);
        }
    }
}