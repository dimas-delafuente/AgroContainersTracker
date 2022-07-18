using System.Threading;
using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Domain
{
    public interface IInputRepository : IRepository<Input>
    {
        Task<List<Input>> GetByCampaignIdAsync(int campaignId, CancellationToken cancellationToken);
        Task<Input> AddAsync(Input Input, CancellationToken cancellationToken);
        Task<Input> GetAsync(int campaignId, int inputId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int campaignId, int inputId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Input Input, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int campaignId, int inputId, CancellationToken cancellationToken);
        Task<bool> CloseEntryAsync(int campaignId, int inputId, CancellationToken cancellationToken);
    }
}