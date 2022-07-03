using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class UpdateColdRoomHandler : ColdRoomBaseHandler, IRequestHandler<UpdateColdRoomCommand, bool>
    {
        public UpdateColdRoomHandler(IColdRoomRepository coldRoomRepository) : base(coldRoomRepository)
        {
        }

        public async Task<bool> Handle(UpdateColdRoomCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            Ensure.Positive(request.ColdRoomId, nameof(request.ColdRoomId));

            return await _coldRoomRepository.UpdateAsync(request.ColdRoomId, request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
