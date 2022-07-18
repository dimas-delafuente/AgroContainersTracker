using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetInputByCampaignIdHandler : InputBaseHandler, IRequestHandler<GetInputByCampaignIdQuery, IEnumerable<Input>>
    {
        public GetInputByCampaignIdHandler(IInputRepository InputRepository) : base(InputRepository)
        {
        }

        public async Task<IEnumerable<Input>> Handle(GetInputByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _InputRepository.GetByCampaignIdAsync(request.CampaignId, cancellationToken).ConfigureAwait(false);
        }
    }
}
