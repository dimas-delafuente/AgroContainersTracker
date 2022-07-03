using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class DeleteColdRoomCommand : IRequest<bool>
    {
        public int ColdRoomId { get; set; }

        public DeleteColdRoomCommand(int coldRoomId)
        {
            Ensure.Positive(coldRoomId, nameof(coldRoomId));
            ColdRoomId = coldRoomId;
        }
    }
}
