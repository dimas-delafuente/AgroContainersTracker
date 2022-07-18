using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Domain
{
    public interface IRateRepository : IRepository<Rate>
    {
        Task<IEnumerable<Rate>> GetAllAsync(CancellationToken cancellationToken);
        Task<Rate> GetByIdAsync(int rateId, CancellationToken cancellationToken);
        //Task<RateDetails> GetDetailsByIdAsync(int rateId);
        Task<bool> DeleteAsync(int rateId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(int rateId, Rate rate, CancellationToken cancellationToken);
        Task<Rate> AddAsync(Rate rate, CancellationToken cancellationToken);
        Task<bool> ExistsRateNameAsync(string name, CancellationToken cancellationToken);
    }
}
