using AgroContainerTracker.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetFruitByIdHandler : FruitsBaseHandler, IRequestHandler<GetFruitByIdQuery, Fruit>
    {
        public GetFruitByIdHandler(IFruitRepository fruitRepository) : base(fruitRepository)
        {
        }

        public async Task<Fruit> Handle(GetFruitByIdQuery request, CancellationToken cancellationToken)
        {
            return await _fruitRepository.GetByIdAsync(request.FruitId, cancellationToken).ConfigureAwait(false);
        }
    }
}
