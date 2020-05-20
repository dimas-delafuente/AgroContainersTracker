using System;
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
    public class CreditorService : ICreditorService
    {
        private readonly ILogger<CreditorService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CreditorService(ApplicationContext context, IMapper mapper, ILogger<CreditorService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Creditor> AddAsync(AddCreditorRequest creditor)
        {
            try
            {
                if (creditor == null)
                    throw new ArgumentNullException();

                CreditorEntity entity = _mapper.Map<CreditorEntity>(creditor);

                var addResponse = await _context.Creditors.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<Creditor>(addResponse.Entity) : null;
                }
                    
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Creditor: {creditor}", e.Message, JsonSerializer.Serialize(creditor));
            }

            return null;
        }

        public async Task<List<Creditor>> GetAllAsync()
        {
            try
            {
                IEnumerable<CreditorEntity> entities = await _context.Creditors
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<Creditor>>(entities);

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all Creditors", e.Message);
            }

            return new List<Creditor>();
        }

        public async Task<Creditor> GetByIdAsync(int creditorId)
        {
            try
            {
                if (creditorId < 0)
                    throw new ArgumentOutOfRangeException();

                CreditorEntity entity = await _context.Creditors
                    .AsNoTracking()
                    .Include(x => x.Country)
                    .FirstOrDefaultAsync(x => x.CreditorId.Equals(creditorId))
                    .ConfigureAwait(false);

                return _mapper.Map<Creditor>(entity);

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving Creditor with Id: {creditorId}", e.Message, creditorId);
            }

            return null;
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
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while removing Creditor with Id: {creditorId}", e.Message, creditorId);
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Creditor creditor)
        {
            try
            {
                if (creditor == null)
                    throw new ArgumentNullException();

                CreditorEntity entity = await _context.Creditors
                    .FindAsync(creditor.CreditorId)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(creditor, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Creditor: {creditor}", e.Message, JsonSerializer.Serialize(creditor));
            }

            return false;
        }
    }
}
