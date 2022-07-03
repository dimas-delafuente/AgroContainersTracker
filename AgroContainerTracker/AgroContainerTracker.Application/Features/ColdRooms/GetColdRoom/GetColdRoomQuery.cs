using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class GetColdRoomQuery : IRequest<ColdRoom>
    {
        public int ColdRoomId { get; set; }

        public GetColdRoomQuery(int coldRoomId)
        {
            Ensure.Positive(coldRoomId, nameof(coldRoomId));
            ColdRoomId = coldRoomId;
        }
    }
}
