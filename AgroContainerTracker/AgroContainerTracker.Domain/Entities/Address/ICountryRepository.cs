using AgroContainerTracker.Domain.Common;

namespace AgroContainerTracker.Domain.Entities
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken);
    }
}
