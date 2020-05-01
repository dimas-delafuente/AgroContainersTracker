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
                throw;
            }
        }

        public async Task<List<Carrier>> GetAllAsync()
        {
            IEnumerable<CarrierEntity> entities = await _context.Carriers.AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<Carrier>>(entities);
        }

        public async Task<Carrier> GetByIdAsync(int carrierId)
        {
            if (carrierId < 0)
                throw new ArgumentOutOfRangeException();

            CarrierEntity entity = await _context.Carriers
                .Include(x => x.Country)
                .Include(x => x.Vehicles)
                .Include(x => x.Drivers)
                .FirstOrDefaultAsync(x => x.CarrierId.Equals(carrierId))
                .ConfigureAwait(false);

            return _mapper.Map<Carrier>(entity);
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
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
