using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class CarrierService : ICarrierService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CarrierService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task AddAsync(AddCarrierRequest carrier)
        {
            try
            {
                if (carrier == null)
                    throw new ArgumentNullException();

                CarrierEntity entity = _mapper.Map<CarrierEntity>(carrier);

                var addResponse = await _context.Carriers.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                    await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                _context.DetachAll();
                throw;
            }
        }

        public async Task<List<Carrier>> GetAllAsync()
        {
            IEnumerable<CarrierEntity> entities = await _context.Carriers
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<Carrier>>(entities);
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
            catch (Exception)
            {
                _context.DetachAll();
                throw;
            }

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
                return false;
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

                return false;
            }
            catch (Exception e)
            {
                _context.DetachAll();
                throw;
            }
        }
    }
}
