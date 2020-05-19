using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Fruits;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class FruitService : IFruitService
    {
        private readonly ILogger<FruitService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public FruitService(ApplicationContext context, IMapper mapper, ILogger<FruitService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Fruit> AddAsync(AddFruitRequest fruit)
        {
            try
            {
                if (fruit == null)
                    throw new ArgumentNullException();

                FruitEntity entity = _mapper.Map<FruitEntity>(fruit);

                var addResponse = await _context.Fruits.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<Fruit>(addResponse.Entity) : null;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Fruit: {fruit}", e.Message, JsonSerializer.Serialize(fruit));
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int fruitId)
        {
            try
            {
                FruitEntity fruit = await _context.Fruits.FindAsync(fruitId).ConfigureAwait(false);

                if (fruit != null)
                {
                    var removedEntity = _context.Fruits.Remove(fruit);
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
                _logger.LogError(e, "Exception: {e} // Internal Error while deleting Fruit: {fruitId}", e.Message, fruitId);
            }

            return false;
        }

        public async Task<List<Fruit>> GetAllAsync()
        {
            try
            {
                IEnumerable<FruitEntity> entities = await _context.Fruits
                    .AsNoTracking()
                    .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<Fruit>>(entities);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all fruits.", e.Message);
            }

            return new List<Fruit>();
        }

        public async Task<Fruit> GetByIdAsync(int fruitId)
        {
            try
            {
                if (fruitId < 0)
                    throw new ArgumentOutOfRangeException();

                FruitEntity entity = await _context.Fruits
                   .AsNoTracking()
                   .FirstOrDefaultAsync(x => x.FruitId.Equals(fruitId))
                   .ConfigureAwait(false);


                return _mapper.Map<Fruit>(entity);

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving fruit with Id: {fruitId}", e.Message, fruitId);
            }

            return null;
        }

        public async Task<bool> UpdateAsync(Fruit fruit)
        {
            try
            {
                if (fruit == null)
                    throw new ArgumentNullException();

                FruitEntity entity = await _context.Fruits
                    .FindAsync(fruit.FruitId)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(fruit, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Fruit: {fruit}", e.Message, JsonSerializer.Serialize(fruit));
            }

            return false;
        }
    }
}
