using System.Threading;
using System.Threading.Tasks;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    internal class DeleteInputHandler : InputBaseHandler, IRequestHandler<DeleteInputCommand, bool>
    {
        public DeleteInputHandler(IInputRepository InputRepository) : base(InputRepository) {}
        public async Task<bool> Handle(DeleteInputCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _InputRepository.DeleteAsync(request.CampaignId, request.InputNumber, cancellationToken).ConfigureAwait(false);
        }
    }
}