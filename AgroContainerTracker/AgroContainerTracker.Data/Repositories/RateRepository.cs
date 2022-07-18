using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Data.Repositories
{
    internal class RateRepository : IRateRepository
    {
        private readonly ApplicationContext _context;

        public RateRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<Rate> AddAsync(Rate rate, CancellationToken cancellationToken)
        {
            Ensure.NotNull(rate, nameof(rate));

            try
            {
                var addResponse = await _context.Rates.AddAsync(rate, cancellationToken).ConfigureAwait(false);
                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
                    return created ? addResponse.Entity : null;
                }
            }
            catch
            {
                _context.DetachAll();
                throw;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int rateId, CancellationToken cancellationToken)
        {
            Ensure.Positive(rateId, nameof(rateId));

            try
            {
                var rate = await _context.Rates.FindAsync(new object[] { rateId }, cancellationToken: cancellationToken).ConfigureAwait(false);
                if (rate != null)
                {
                    var removedEntity = _context.Rates.Remove(rate);
                    if (removedEntity?.Entity != null && removedEntity.State.Equals(EntityState.Deleted))
                    {
                        return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
                    }
                }
            }
            catch
            {
                _context.DetachAll();
                throw;
            }

            return false;
        }

        public Task<bool> ExistsRateNameAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Rate>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Rates
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Rate> GetByIdAsync(int rateId, CancellationToken cancellationToken)
        {
            Ensure.Positive(rateId, nameof(rateId));
            return await _context.Rates
               .AsNoTracking()
               .Where(x => x.Id.Equals(rateId))
               .FirstOrDefaultAsync(cancellationToken)
               .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(int rateId, Rate rate, CancellationToken cancellationToken)
        {
            Ensure.Positive(rateId, nameof(rateId));
            Ensure.NotNull(rate, nameof(rate));

            try
            {
                var entity = await _context.Rates
                    .FindAsync(new object[] { rate.Id }, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    entity.MainPrice = rate.MainPrice;
                    entity.SecondaryPrice = rate.SecondaryPrice;
                    entity.Name = rate.Name;
                    entity.Description = rate.Description;

                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    return true;
                }
            }
            catch
            {
                _context.DetachAll();
                throw;
            }

            return false;
        }
    }
}
