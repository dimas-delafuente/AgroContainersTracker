using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Data.Repositories
{
    internal class StorageRepository : IStorageRepository
    {
        private readonly ApplicationContext _context;

        public StorageRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<Storage> AddAsync(Storage storage, CancellationToken cancellationToken)
        {
            Ensure.NotNull(storage, nameof(storage));

            try
            {
                var addResponse = await _context.Storages.AddAsync(storage, cancellationToken).ConfigureAwait(false);
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

        public async Task<bool> DeleteAsync(int storageId, CancellationToken cancellationToken)
        {
            Ensure.Positive(storageId, nameof(storageId));

            try
            {
                var storage = await _context.Storages.FindAsync(new object[] { storageId }, cancellationToken).ConfigureAwait(false);
                if (storage != null)
                {
                    var removedEntity = _context.Storages.Remove(storage);
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

        public async Task<bool> ExistsByStorageNumber(int storageNumber, CancellationToken cancellationToken)
        {
            Ensure.Positive(storageNumber, nameof(storageNumber));

            return await _context.Storages
                .AnyAsync(x => x.Number.Equals(storageNumber), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Storage>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Storages
                .AsNoTracking()
                .OrderBy(x => x.Number)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Storage> GetByIdAsync(int storageId, CancellationToken cancellationToken)
        {
            Ensure.Positive(storageId, nameof(storageId));

            return await _context.Storages
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id.Equals(storageId), cancellationToken)
               .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(int storageId, Storage storage, CancellationToken cancellationToken)
        {
            Ensure.Positive(storageId, nameof(storageId));
            Ensure.NotNull(storage, nameof(storage));

            try
            {
                var entity = _context.Storages.Update(storage);

                if (entity != null)
                {
                    return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
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
