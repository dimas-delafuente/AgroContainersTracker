using System.Threading;
using System.Threading.Tasks;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    internal class UpdateInputHandler : InputBaseHandler, IRequestHandler<UpdateInputCommand, bool>
    {
        public UpdateInputHandler(IInputRepository InputRepository) : base(InputRepository) {}

        public async Task<bool> Handle(UpdateInputCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _InputRepository.UpdateAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}