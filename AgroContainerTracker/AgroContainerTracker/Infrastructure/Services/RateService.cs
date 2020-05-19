using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Rates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class RateService : IRateService
    {
        private readonly ILogger<RateService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public RateService(ApplicationContext context, IMapper mapper, ILogger<RateService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Rate> AddAsync(AddRateRequest rate)
        {
            try
            {
                if (rate == null)
                    throw new ArgumentNullException();

                RateEntity entity = _mapper.Map<RateEntity>(rate);

                var addResponse = await _context.Rates.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<Rate>(addResponse.Entity) : null;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Rate: {rate}", e.Message, JsonSerializer.Serialize(rate));
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int rateId)
        {
            try
            {
                RateEntity rate = await _context.Rates.FindAsync(rateId).ConfigureAwait(false);

                if (rate != null)
                {
                    var removedEntity = _context.Rates.Remove(rate);
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
                _logger.LogError(e, "Exception: {e} // Internal Error while deleting Rate: {rateId}", e.Message, rateId);
            }

            return false;
        }

        public async Task<List<Rate>> GetAllAsync()
        {
            try
            {
                IEnumerable<RateEntity> entities = await _context.Rates
                    .AsNoTracking()
                    .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<Rate>>(entities);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all rates.", e.Message);
            }

            return new List<Rate>();
        }

        public async Task<Rate> GetByIdAsync(int rateId)
        {
            try
            {
                if (rateId < 0)
                    throw new ArgumentOutOfRangeException();

                RateEntity entity = await _context.Rates
                    .AsNoTracking()
                   .FirstOrDefaultAsync(x => x.RateId.Equals(rateId))
                   .ConfigureAwait(false);


                return _mapper.Map<Rate>(entity);

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving rate with Id: {rateId}", e.Message, rateId);
            }

            return null;
        }

        public async Task<RateDetails> GetDetailsByIdAsync(int rateId)
        {
            if (rateId < 0)
                throw new ArgumentOutOfRangeException();

            try
            {
                var entity = await _context.Rates
                    .AsNoTracking()
                    .AsQueryable()
                    .Where(x => x.RateId.Equals(rateId))
                     .Select(x => new
                     {
                         Rate = x,
                         Customers = x.Customers.Select(c => new CustomerEntity
                         {
                             CustomerId = c.CustomerId,
                             CustomerNumber = c.CustomerNumber,
                             Name = c.Name
                         })
                     })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                var rate = entity.Rate;
                rate.Customers = (ICollection<CustomerEntity>)entity.Customers;

                return _mapper.Map<RateDetails>(rate);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving rate details with Id: {rateId}", e.Message, rateId);
            }

            return null;
        }

        public async Task<bool> UpdateAsync(Rate rate)
        {
            try
            {
                if (rate == null)
                    throw new ArgumentNullException();

                RateEntity entity = await _context.Rates
                    .FindAsync(rate.RateId)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(rate, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Rate: {rate}", e.Message, JsonSerializer.Serialize(rate));
            }

            return false;
        }
    }
}
