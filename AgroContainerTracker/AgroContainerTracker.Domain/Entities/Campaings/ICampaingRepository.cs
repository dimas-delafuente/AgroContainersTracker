namespace AgroContainerTracker.Domain
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<IEnumerable<Campaign>> GetAllAsync(CancellationToken cancellationToken);
    }
}