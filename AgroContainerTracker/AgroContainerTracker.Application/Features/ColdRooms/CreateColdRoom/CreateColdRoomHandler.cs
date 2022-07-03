using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class CreateColdRoomHandler : ColdRoomBaseHandler, IRequestHandler<CreateColdRoomCommand, ColdRoom>
    {
        public CreateColdRoomHandler(IColdRoomRepository coldRoomRepository) : base(coldRoomRepository)
        {

        }

        public async Task<ColdRoom> Handle(CreateColdRoomCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            var newColdRoom = await _coldRoomRepository.AddAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
            return newColdRoom;
        }
    }
}
