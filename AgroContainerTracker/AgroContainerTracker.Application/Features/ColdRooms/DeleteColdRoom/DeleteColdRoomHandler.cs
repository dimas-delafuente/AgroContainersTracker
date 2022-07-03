using AgroContainerTracker.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class DeleteColdRoomHandler : ColdRoomBaseHandler, IRequestHandler<DeleteColdRoomCommand, bool>
    {
        public DeleteColdRoomHandler(IColdRoomRepository coldRoomRepository) : base(coldRoomRepository)
        {

        }

        public async Task<bool> Handle(DeleteColdRoomCommand request, CancellationToken cancellationToken)
        {
            return await _coldRoomRepository.DeleteAsync(request.ColdRoomId, cancellationToken).ConfigureAwait(false);
        }
    }
}
