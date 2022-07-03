using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Data.Repositories
{
    internal class PackagingRepository : IPackagingRepository
    {
        private readonly ApplicationContext _context;

        public PackagingRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<Packaging> AddAsync(Packaging packaging, CancellationToken cancellationToken)
        {
            Ensure.NotNull(packaging, nameof(packaging));

            try
            {
                var addResponse = await _context.Packagings.AddAsync(packaging, cancellationToken).ConfigureAwait(false);

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

        public async Task<bool> DeleteAsync(int packagingId, CancellationToken cancellationToken)
        {
            try
            {
                var packaging = await _context.Packagings.FindAsync(packagingId, cancellationToken).ConfigureAwait(false);

                if (packaging != null)
                {
                    var removedEntity = _context.Packagings.Remove(packaging);
                    if (removedEntity?.Entity != null && removedEntity.State.Equals(EntityState.Deleted))
                    {
                        int deleted = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        return deleted > 0;
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

        public async Task<bool> ExistsPackagingCodeAsync(string packagingCode, CancellationToken cancellationToken)
        {
            Ensure.NotEmpty(packagingCode, nameof(packagingCode));
            return await _context.Packagings.AnyAsync(x => x.Code.Equals(packagingCode), cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Packaging>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Packagings
                .AsNoTracking()
                .Include(x => x.Owner)
                .OrderBy(x => x.Code)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Packaging> GetByIdAsync(int packagingId, CancellationToken cancellationToken)
        {
            Ensure.Positive(packagingId, nameof(packagingId));
            return await _context.Packagings
                .AsNoTracking()
                .Include(x => x.Owner)
                .Include(x => x.PackagingMovements)
                    .ThenInclude(pm => pm.Customer)
               .FirstOrDefaultAsync(x => x.PackagingId.Equals(packagingId), cancellationToken)
               .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(int packagingId, Packaging packaging, CancellationToken cancellationToken)
        {
            Ensure.Positive(packagingId, nameof(packagingId));
            Ensure.NotNull(packaging, nameof(packaging));

            try
            {
                var entity = await _context.Packagings
                    .FirstOrDefaultAsync(x => x.PackagingId.Equals(packagingId), cancellationToken)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    entity.Active = packaging.Active;
                    entity.Code = packaging.Code;
                    entity.Color = packaging.Color;
                    entity.Description = packaging.Description;
                    entity.Total = packaging.Total;
                    entity.Type = packaging.Type;
                    entity.Material = packaging.Material;
                    entity.Weight = packaging.Weight;
                    entity.CustomerId = packaging.CustomerId;
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
