using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Application.Features
{
    internal abstract class InputBaseHandler
    {
        protected readonly IInputRepository _InputRepository;

        protected InputBaseHandler(IInputRepository InputRepository)
        {
            Ensure.NotNull(InputRepository, nameof(InputRepository));
            _InputRepository = InputRepository;
        }
    }
}
