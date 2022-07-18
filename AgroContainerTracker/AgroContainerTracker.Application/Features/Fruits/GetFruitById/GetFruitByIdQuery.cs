using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class GetFruitByIdQuery : IRequest<Fruit>
    {
        public int FruitId { get; set; }

        public GetFruitByIdQuery(int fruitId)
        {
            Ensure.Positive(fruitId, nameof(fruitId));
            FruitId = fruitId;
        }
    }
}
