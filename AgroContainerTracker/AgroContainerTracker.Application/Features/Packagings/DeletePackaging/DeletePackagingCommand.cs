using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class DeletePackagingCommand : IRequest<bool>
    {
        public int PackagingId { get; set; }
        public DeletePackagingCommand(int packagingId)
        {
            Ensure.Positive(packagingId, nameof(packagingId));
            PackagingId = packagingId;
        }
    }
}
