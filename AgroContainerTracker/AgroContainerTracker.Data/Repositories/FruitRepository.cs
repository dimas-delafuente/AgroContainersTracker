using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Data.Repositories
{
    internal class FruitRepository : IFruitRepository
    {
        private readonly ApplicationContext _context;

        public FruitRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<Fruit> AddAsync(Fruit fruit, CancellationToken cancellationToken)
        {
            Ensure.NotNull(fruit, nameof(fruit));

            try
            {
                var addResponse = await _context.Fruits.AddAsync(fruit, cancellationToken).ConfigureAwait(false);

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

        public async Task<bool> DeleteAsync(int fruitId, CancellationToken cancellationToken)
        {
            Ensure.Positive(fruitId, nameof(fruitId));

            try
            {
                var fruit = await _context.Fruits.FindAsync(new object[] { fruitId }, cancellationToken).ConfigureAwait(false);
                if (fruit != null)
                {
                    var removedEntity = _context.Fruits.Remove(fruit);
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

        public async Task<IEnumerable<Fruit>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Fruits
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Fruit> GetByIdAsync(int fruitId, CancellationToken cancellationToken)
        {
            Ensure.Positive(fruitId, nameof(fruitId));

            return await _context.Fruits
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(fruitId), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(int fruitId, Fruit fruit, CancellationToken cancellationToken)
        {
            Ensure.Positive(fruitId, nameof(fruitId));
            Ensure.NotNull(fruit, nameof(fruit));

            try
            {
                var entity = await _context.Fruits
                    .FindAsync(new object[] { fruit.Id }, cancellationToken)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    entity.Description = fruit.Description;
                    entity.Code = fruit.Code;
                    entity.Name = fruit.Name;

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
