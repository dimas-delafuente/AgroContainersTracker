using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationContext _context;

        public CountryRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Countries
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
