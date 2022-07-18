using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class ExistsStorageNumberQuery : IRequest<bool>
    {
        public int StorageNumber { get; set; }

        public ExistsStorageNumberQuery(int storageNumber)
        {
            Ensure.Positive(storageNumber, nameof(storageNumber));
            StorageNumber = storageNumber;
        }
    }
}
