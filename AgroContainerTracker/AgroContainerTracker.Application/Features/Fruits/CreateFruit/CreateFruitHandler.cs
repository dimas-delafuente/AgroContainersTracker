using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class CreateFruitHandler : FruitsBaseHandler, IRequestHandler<CreateFruitCommand, Fruit>
    {
        public CreateFruitHandler(IFruitRepository fruitRepository) : base(fruitRepository)
        {
        }

        public async Task<Fruit> Handle(CreateFruitCommand request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));
            return await _fruitRepository.AddAsync(request.ToDomain(), cancellationToken).ConfigureAwait(false);
        }
    }
}
