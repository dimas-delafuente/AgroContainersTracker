using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class GetPackagingByIdQuery : IRequest<Packaging>
    {
        public int PackagingId { get; set; }
        public GetPackagingByIdQuery(int packagingId)
        {
            Ensure.Positive(packagingId, nameof(packagingId));
            PackagingId = packagingId;
        }
    }
}
