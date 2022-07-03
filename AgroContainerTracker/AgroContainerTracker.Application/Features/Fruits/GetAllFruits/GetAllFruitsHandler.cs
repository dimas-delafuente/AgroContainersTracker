using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Application.Features
{
    internal class GetAllFruitsHandler : FruitsBaseHandler, IRequestHandler<GetAllFruitsQuery, IEnumerable<Fruit>>
    {
        public GetAllFruitsHandler(IFruitRepository fruitRepository) : base(fruitRepository)
        {
        }

        public async Task<IEnumerable<Fruit>> Handle(GetAllFruitsQuery request, CancellationToken cancellationToken)
        {
            Ensure.NotNull(request, nameof(request));

            return await _fruitRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
