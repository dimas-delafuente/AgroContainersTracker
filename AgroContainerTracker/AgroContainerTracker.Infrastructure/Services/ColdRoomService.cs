using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Core.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class ColdRoomService : IColdRoomService
    {

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ColdRoomService> _logger;

        public ColdRoomService(ApplicationContext context, IMapper mapper, ILogger<ColdRoomService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ColdRoom> AddAsync(AddColdRoomRequest coldRoom)
        {
            try
            {
                if (coldRoom == null)
                    throw new ArgumentNullException();

                ColdRoomEntity entity = _mapper.Map<ColdRoomEntity>(coldRoom);

                var addResponse = await _context.ColdRooms.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<ColdRoom>(addResponse.Entity) : null;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Cold Room: {coldRoom}", e.Message, JsonSerializer.Serialize(coldRoom));
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int coldRoomId)
        {
            try
            {
                ColdRoomEntity coldRoom = await _context.ColdRooms.FindAsync(coldRoomId).ConfigureAwait(false);

                if (coldRoom != null)
                {
                    var removedEntity = _context.ColdRooms.Remove(coldRoom);
                    if (removedEntity?.Entity != null && removedEntity.State.Equals(EntityState.Deleted))
                    {
                        int deleted = await _context.SaveChangesAsync().ConfigureAwait(false);
                        return deleted > 0;
                    }
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while deleting ColdRoom: {coldRoomId}", e.Message, coldRoomId);
            }

            return false;
        }

        public async Task<List<ColdRoom>> GetAllAsync()
        {
            try
            {
                IEnumerable<ColdRoomEntity> entities = await _context.ColdRooms
                    .AsNoTracking()
                    .OrderBy(x => x.Number)
                    .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<ColdRoom>>(entities);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all cold rooms.", e.Message);
            }

            return new List<ColdRoom>();
        }

        public async Task<ColdRoom> GetByIdAsync(int coldRoomId)
        {
            try
            {
                if (coldRoomId < 0)
                    throw new ArgumentOutOfRangeException();

                ColdRoomEntity entity = await _context.ColdRooms
                    .AsNoTracking()
                   .FirstOrDefaultAsync(x => x.ColdRoomId.Equals(coldRoomId))
                   .ConfigureAwait(false);


                return _mapper.Map<ColdRoom>(entity);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving ColdRoom with Id: {coldRoomId}", e.Message, coldRoomId);
            }

            return null;
        }

        public async Task<bool> UpdateAsync(ColdRoom coldRoom)
        {
            try
            {
                if (coldRoom == null)
                    throw new ArgumentNullException();

                ColdRoomEntity entity = await _context.ColdRooms
                    .FindAsync(coldRoom.ColdRoomId)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(coldRoom, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Cold Room: {coldRoom}", e.Message, JsonSerializer.Serialize(coldRoom));
            }

            return false;
        }

        public async Task<bool> ExistsAsync(int coldRoomNumber)
        {
            if (coldRoomNumber < 0)
                return false;

            return await _context.ColdRooms
                .AnyAsync(x => x.Number.Equals(coldRoomNumber))
                .ConfigureAwait(false);
        }
    }
}
