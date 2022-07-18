using AgroContainerTracker.Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetColdAllRoomHandler : StorageBaseHandler, IRequestHandler<GetColdAllRoomsQuery, IEnumerable<Storage>>
    {
        public GetColdAllRoomHandler(IStorageRepository storageRepository) : base(storageRepository)
        {

        }

        public async Task<IEnumerable<Storage>> Handle(GetColdAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _storageRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
