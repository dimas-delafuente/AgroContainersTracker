using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Application.Features
{
    internal abstract class RatesBaseHandler
    {
        protected readonly IRateRepository _rateRepository;
        public RatesBaseHandler(IRateRepository rateRepository)
        {
            Ensure.NotNull(rateRepository, nameof(rateRepository));
            _rateRepository = rateRepository;
        }
    }
}
