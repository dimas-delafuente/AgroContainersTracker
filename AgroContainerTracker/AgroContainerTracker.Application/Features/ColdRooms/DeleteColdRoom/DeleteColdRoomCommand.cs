using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class DeleteStorageCommand : IRequest<bool>
    {
        public int StorageId { get; set; }

        public DeleteStorageCommand(int storageId)
        {
            Ensure.Positive(storageId, nameof(storageId));
            StorageId = storageId;
        }
    }
}
