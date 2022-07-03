using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class ExistsColdRoomHandler : ColdRoomBaseHandler, IRequestHandler<ExistsColdRoomNumberQuery, bool>
    {
        public ExistsColdRoomHandler(IColdRoomRepository coldRoomRepository) : base(coldRoomRepository)
        {

        }

        public async Task<bool> Handle(ExistsColdRoomNumberQuery request, CancellationToken cancellationToken)
        {
            return await _coldRoomRepository.ExistsColdRoomNumberAsync(request.ColdRoomNumber, cancellationToken).ConfigureAwait(false);
        }
    }
}
