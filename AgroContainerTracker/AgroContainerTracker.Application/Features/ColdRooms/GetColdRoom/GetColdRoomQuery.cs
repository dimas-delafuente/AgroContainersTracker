using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class GetStorageQuery : IRequest<Storage>
    {
        public int StorageId { get; set; }

        public GetStorageQuery(int storageId)
        {
            Ensure.Positive(storageId, nameof(storageId));
            StorageId = storageId;
        }
    }
}
