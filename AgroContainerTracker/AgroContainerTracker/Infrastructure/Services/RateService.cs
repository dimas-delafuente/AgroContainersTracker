using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Rates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class RateService : IRateService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public RateService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(AddRateRequest rate)
        {
            try
            {
                if (rate == null)
                    throw new ArgumentNullException();

                RateEntity entity = _mapper.Map<RateEntity>(rate);

                var addResponse = await _context.Rates.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                    await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                throw;
            }
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
                return false;
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
                throw;
            }
        }

        public async Task<Rate> GetByIdAsync(int rateId)
        {
            if (rateId < 0)
                throw new ArgumentOutOfRangeException();

             RateEntity entity = await _context.Rates
                .FindAsync(rateId)
                .ConfigureAwait(false);

            return _mapper.Map<Rate>(entity);
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

                return false;
            }
            catch (Exception)
            {
                _context.DetachAll();
                throw;
            }
        }
    }
}
