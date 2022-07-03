using AgroContainerTracker.Shared;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class DeleteFruitCommand : IRequest<bool>
    {
        public int FruitId { get; set; }
        public DeleteFruitCommand(int fruitId)
        {
            Ensure.Positive(fruitId, nameof(fruitId));
            FruitId = fruitId;
        }
    }
}
