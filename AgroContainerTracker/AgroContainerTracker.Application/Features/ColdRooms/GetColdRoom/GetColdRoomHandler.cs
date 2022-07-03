using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetColdRoomHandler : ColdRoomBaseHandler, IRequestHandler<GetColdRoomQuery, ColdRoom>
    {
        public GetColdRoomHandler(IColdRoomRepository coldRoomRepository) : base(coldRoomRepository)
        {

        }

        public async Task<ColdRoom> Handle(GetColdRoomQuery request, CancellationToken cancellationToken)
        {
            return await _coldRoomRepository.GetByIdAsync(request.ColdRoomId, cancellationToken).ConfigureAwait(false);
        }
    }
}
