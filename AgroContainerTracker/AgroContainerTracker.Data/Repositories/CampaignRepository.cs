using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;

namespace AgroContainerTracker.Data.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationContext _context;

        public CampaignRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Campaigns
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch
            {
                _context.DetachAll();
            }

            return Enumerable.Empty<Campaign>();
        }
    }
}