using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetColdAllRoomHandler : ColdRoomBaseHandler, IRequestHandler<GetColdAllRoomsQuery, IEnumerable<ColdRoom>>
    {
        public GetColdAllRoomHandler(IColdRoomRepository coldRoomRepository) : base(coldRoomRepository)
        {

        }

        public async Task<IEnumerable<ColdRoom>> Handle(GetColdAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _coldRoomRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
