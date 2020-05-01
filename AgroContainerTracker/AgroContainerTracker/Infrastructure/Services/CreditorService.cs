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
    public class CreditorService : ICreditorService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CreditorService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(AddCreditorRequest creditor)
        {
            try
            {
                if (creditor == null)
                    throw new ArgumentNullException();

                CreditorEntity entity = _mapper.Map<CreditorEntity>(creditor);

                var addResponse = await _context.Creditors.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                    await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {

            }
        }

        public async Task<IEnumerable<Creditor>> GetAllAsync()
        {
            IEnumerable<CreditorEntity> entities = await _context.Creditors.AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Creditor>>(entities);
        }

        public async Task<Creditor> GetByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();

            CreditorEntity entity = await _context.Creditors
                .Where(x => x.CompanyId.Equals(id))
                .Include(x => x.Country)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return _mapper.Map<Creditor>(entity);
        }
    }
}
