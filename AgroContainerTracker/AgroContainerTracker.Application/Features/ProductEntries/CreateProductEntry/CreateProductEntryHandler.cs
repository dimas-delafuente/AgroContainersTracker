using System.Threading;
using System.Threading.Tasks;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    internal class CreateInputHandler : InputBaseHandler, IRequestHandler<CreateInputCommand, Input>
    {
        public CreateInputHandler(IInputRepository InputRepository) : base(InputRepository) {}

        public async Task<Input> Handle(CreateInputCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _InputRepository.AddAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}