using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetInputHandler : InputBaseHandler, IRequestHandler<GetInputQuery, Input>
    {
        public GetInputHandler(IInputRepository InputRepository) : base(InputRepository)
        {
        }

        public async Task<Input> Handle(GetInputQuery request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _InputRepository.GetAsync(request.CampaignId, request.InputNumber, cancellationToken).ConfigureAwait(false);
        }
    }
}
