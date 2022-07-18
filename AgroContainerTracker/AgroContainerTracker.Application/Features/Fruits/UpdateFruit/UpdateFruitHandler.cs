using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class UpdateFruitHandler : FruitsBaseHandler, IRequestHandler<UpdateFruitCommand, bool>
    {
        public UpdateFruitHandler(IFruitRepository fruitRepository) : base(fruitRepository)
        {
        }

        public async Task<bool> Handle(UpdateFruitCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            Ensure.Positive(request.FruitId, nameof(request.FruitId));
            return await _fruitRepository.UpdateAsync(request.FruitId, request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
