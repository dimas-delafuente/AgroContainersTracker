using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class CreatePackagingHandler : PackagingsBaseHandler, IRequestHandler<CreatePackagingCommand, Packaging>
    {
        public CreatePackagingHandler(IPackagingRepository packagingRepository) : base(packagingRepository)
        {
        }

        public async Task<Packaging> Handle(CreatePackagingCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _packagingRepository.AddAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
