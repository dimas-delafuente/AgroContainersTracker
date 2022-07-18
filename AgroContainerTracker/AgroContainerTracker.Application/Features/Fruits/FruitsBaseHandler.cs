using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Application.Features
{
    internal abstract class FruitsBaseHandler
    {
        protected readonly IFruitRepository _fruitRepository;

        public FruitsBaseHandler(IFruitRepository fruitRepository)
        {
            Ensure.NotNull(fruitRepository, nameof(fruitRepository));
            _fruitRepository = fruitRepository;
        }
    }
}
