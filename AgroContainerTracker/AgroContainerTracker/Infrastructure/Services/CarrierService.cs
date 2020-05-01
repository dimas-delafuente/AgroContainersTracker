using System;
using System.Collections.Generic;
using System.Linq;
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
            catch (Exception e)
            {

            }
        }

        public async Task<IEnumerable<Carrier>> GetAllAsync()
        {
            IEnumerable<CarrierEntity> entities = await _context.Carriers.AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Carrier>>(entities);
        }

        public async Task<Carrier> GetByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();

            CarrierEntity entity = await _context.Carriers
                .Where(x => x.CompanyId.Equals(id))
                .Include(x => x.Country)
                .Include(x => x.Vehicles)
                .Include(x => x.Drivers)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return _mapper.Map<Carrier>(entity);
        }
    }
}
