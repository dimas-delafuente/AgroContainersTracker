using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Data.Repositories
{
    internal class ColdRoomRepository : IColdRoomRepository
    {
        private readonly ApplicationContext _context;

        public ColdRoomRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<ColdRoom> AddAsync(ColdRoom coldRoom, CancellationToken cancellationToken)
        {
            Ensure.NotNull(coldRoom, nameof(coldRoom));

            try
            {
                if (coldRoom == null)
                    throw new ArgumentNullException(nameof(coldRoom));

                var addResponse = await _context.ColdRooms.AddAsync(coldRoom, cancellationToken).ConfigureAwait(false);
                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
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

        public async Task<bool> DeleteAsync(int coldRoomId, CancellationToken cancellationToken)
        {
            Ensure.Positive(coldRoomId, nameof(coldRoomId));

            try
            {
                var coldRoom = await _context.ColdRooms.FindAsync(coldRoomId, cancellationToken).ConfigureAwait(false);
                if (coldRoom != null)
                {
                    var removedEntity = _context.ColdRooms.Remove(coldRoom);
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

        public async Task<bool> ExistsColdRoomNumberAsync(int coldRoomNumber, CancellationToken cancellationToken)
        {
            Ensure.Positive(coldRoomNumber, nameof(coldRoomNumber));

            return await _context.ColdRooms
                .AnyAsync(x => x.Number.Equals(coldRoomNumber), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<ColdRoom>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.ColdRooms
                .AsNoTracking()
                .OrderBy(x => x.Number)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<ColdRoom> GetByIdAsync(int coldRoomId, CancellationToken cancellationToken)
        {
            Ensure.Positive(coldRoomId, nameof(coldRoomId));

            return await _context.ColdRooms
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.ColdRoomId.Equals(coldRoomId), cancellationToken)
               .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(int coldRoomId, ColdRoom coldRoom, CancellationToken cancellationToken)
        {
            Ensure.Positive(coldRoomId, nameof(coldRoomId));
            Ensure.NotNull(coldRoom, nameof(coldRoom));

            try
            {
                var entity = await _context.ColdRooms
                    .FindAsync(coldRoomId, cancellationToken)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    entity.Capacity = coldRoom.Capacity;
                    entity.Description = coldRoom.Description;
                    entity.Humidity = coldRoom.Humidity;
                    entity.Number = coldRoom.Number;
                    entity.Surface = coldRoom.Surface;
                    entity.Temperature = coldRoom.Temperature;

                    await _context.SaveChangesAsync().ConfigureAwait(false);
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
