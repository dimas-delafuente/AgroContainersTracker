using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class ExistsPackagingCodeQuery : IRequest<bool>
    {
        public string PackagingCode { get; set; }
        public ExistsPackagingCodeQuery(string packagingCode)
        {
            Ensure.NotNullOrEmpty(packagingCode, nameof(packagingCode));
            PackagingCode = packagingCode;
        }
    }
}
