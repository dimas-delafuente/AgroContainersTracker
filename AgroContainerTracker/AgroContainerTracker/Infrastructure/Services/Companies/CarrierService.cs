﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class CarrierService : ICarrierService
    {
        private readonly ILogger<CarrierService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CarrierService(ApplicationContext context, IMapper mapper, ILogger<CarrierService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<Carrier> AddAsync(AddCarrierRequest carrier)
        {
            try
            {
                if (carrier == null)
                    throw new ArgumentNullException();

                CarrierEntity entity = _mapper.Map<CarrierEntity>(carrier);

                var addResponse = await _context.Carriers.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<Carrier>(addResponse.Entity) : null;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Carrier: {carrier}", e.Message, JsonSerializer.Serialize(carrier));
            }

            return null;
        }

        public async Task<List<Carrier>> GetAllAsync()
        {
            try
            {
                IEnumerable<CarrierEntity> entities = await _context.Carriers
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<Carrier>>(entities);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all rates.", e.Message);
            }

            return new List<Carrier>();
        }            

        public async Task<Carrier> GetByIdAsync(int carrierId)
        {
            try
            {
                if (carrierId < 0)
                    throw new ArgumentOutOfRangeException();

                CarrierEntity entity = await _context.Carriers
                    .AsNoTracking()
                    .Include(x => x.Country)
                    .Include(x => x.Vehicles)
                    .Include(x => x.Drivers)
                    .FirstOrDefaultAsync(x => x.CarrierId.Equals(carrierId))
                    .ConfigureAwait(false);

                return _mapper.Map<Carrier>(entity);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving Carrier: {carrierId}", e.Message, carrierId);
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int carrierId)
        {
            try
            {
                CarrierEntity carrier = await _context.Carriers.FindAsync(carrierId).ConfigureAwait(false);

                if (carrier != null)
                {
                    var removedEntity = _context.Carriers.Remove(carrier);
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
                _logger.LogError(e, "Exception: {e} // Internal Error while removing Carrier: {carrierId}", e.Message, carrierId);
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Carrier carrier)
        {
            try
            {
                if (carrier == null)
                    throw new ArgumentNullException();

                CarrierEntity entity = await _context.Carriers
                    .Include(x => x.Vehicles)
                    .Include(x => x.Drivers)
                    .FirstOrDefaultAsync(x => x.CarrierId.Equals(carrier.CarrierId))
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(carrier, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Carrier: {carrier}", e.Message, JsonSerializer.Serialize(carrier));
            }

            return false;
        }
    }
}
