using System.Threading;
using System.Threading.Tasks;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    internal class CloseInputHandler : InputBaseHandler, IRequestHandler<CloseInputCommand, bool>
    {
        public CloseInputHandler(IInputRepository InputRepository) : base(InputRepository) { }

        public async Task<bool> Handle(CloseInputCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _InputRepository.CloseEntryAsync(request.CampaignId, request.InputNumber, cancellationToken).ConfigureAwait(false);
        }
    }
}