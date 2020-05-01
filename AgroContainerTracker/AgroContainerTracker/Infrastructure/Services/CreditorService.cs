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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Creditor>> GetAllAsync()
        {
            IEnumerable<CreditorEntity> entities = await _context.Creditors.AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<Creditor>>(entities);
        }

        public async Task<Creditor> GetByIdAsync(int creditorId)
        {
            if (creditorId < 0)
                throw new ArgumentOutOfRangeException();

            CreditorEntity entity = await _context.Creditors
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.CreditorId.Equals(creditorId))
                .ConfigureAwait(false);

            return _mapper.Map<Creditor>(entity);
        }

        public async Task<bool> DeleteAsync(int creditorId)
        {
            try
            {
                CreditorEntity creditor = await _context.Creditors.FindAsync(creditorId).ConfigureAwait(false);

                if (creditor != null)
                {
                    var removedEntity = _context.Creditors.Remove(creditor);
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
