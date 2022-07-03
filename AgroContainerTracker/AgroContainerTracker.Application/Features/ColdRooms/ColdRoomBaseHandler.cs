using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Application.Features
{
    internal abstract class ColdRoomBaseHandler
    {
        protected readonly IColdRoomRepository _coldRoomRepository;

        public ColdRoomBaseHandler(IColdRoomRepository coldRoomRepository)
        {
            Ensure.NotNull(coldRoomRepository, nameof(coldRoomRepository));
            _coldRoomRepository = coldRoomRepository;
        }
    }
}
