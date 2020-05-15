using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Packagings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Operation = AgroContainerTracker.Domain.Packagings.Operation;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class PackagingService : IPackagingService
    {
        private readonly ILogger<PackagingService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public PackagingService(ApplicationContext context, IMapper mapper, ILogger<PackagingService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Packaging> AddAsync(AddPackagingRequest packaging)
        {
            try
            {
                if (packaging == null)
                    throw new ArgumentNullException();

                PackagingEntity entity = _mapper.Map<PackagingEntity>(packaging);

                var addResponse = await _context.Packagings.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<Packaging>(addResponse.Entity) : null;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Packaging: {packaging}",
                    e.Message, JsonSerializer.Serialize(packaging));
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int packagingId)
        {
            try
            {
                PackagingEntity packaging = await _context.Packagings.FindAsync(packagingId).ConfigureAwait(false);

                if (packaging != null)
                {
                    var removedEntity = _context.Packagings.Remove(packaging);
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
                _logger.LogError(e, "Exception: {e} // Internal Error while removing Packaging: {packagingId}",
                    e.Message, packagingId);
            }

            return false;
        }

        public async Task<List<Packaging>> GetAllAsync()
        {
            try
            {      
                IEnumerable<PackagingEntity> packagings = await _context.Packagings
                    .AsNoTracking()
                    .Include(x => x.Owner)
                    .ToListAsync()
                    .ConfigureAwait(false);

                return _mapper.Map<List<Packaging>>(packagings);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all Packagings",
                    e.Message);
            }

            return new List<Packaging>();
        }

        public async Task<Packaging> GetByIdAsync(int packagingId)
        {
            try
            {
                if (packagingId < 0)
                    throw new ArgumentOutOfRangeException();

                var entity = await _context.Packagings
                    .AsNoTracking()
                    .Include(x => x.Owner)
                    .Include(x => x.PackagingMovements)
                        .ThenInclude(pm => pm.Customer)
                   .FirstOrDefaultAsync(x => x.PackagingId.Equals(packagingId))
                   .ConfigureAwait(false);

                return _mapper.Map<Packaging>(entity);

            } catch(Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving Packaging: {packagingId}",
                    e.Message, packagingId);
            }

            return null;
        }

        public async Task<bool> UpdateAsync(Packaging packaging)
        {
            try
            {
                if (packaging == null)
                    throw new ArgumentNullException();

                PackagingEntity entity = await _context.Packagings
                    .FirstOrDefaultAsync(x => x.PackagingId.Equals(packaging.PackagingId))
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(packaging, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Packaging: {packaging}",
                    e.Message, JsonSerializer.Serialize(packaging));
            }

            return false;
        }

        public async Task<Packaging> AddPackagingMovementAsync(AddPackagingMovementRequest packagingMovement)
        {
            try
            {
                if (packagingMovement == null)
                    throw new ArgumentNullException();

                PackagingEntity packaging = await _context.Packagings
                    .FindAsync(packagingMovement.PackagingId)
                    .ConfigureAwait(false);

                if (packaging != null && IsValidAmount(packaging.Total, packagingMovement))
                {
                    packaging.Total = packagingMovement.Operation.Equals(Operation.Add) ?
                        packaging.Total + packagingMovement.Amount :
                        packaging.Total - packagingMovement.Amount;

                    PackagingMovementEntity entity = _mapper.Map<PackagingMovementEntity>(packagingMovement);
                    entity.Total = packaging.Total;

                    var added = await _context.PackagingMovements.AddAsync(entity);

                    if (added.State.Equals(EntityState.Added))
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return _mapper.Map<Packaging>(packaging);
                    }
                }

                _context.DetachAll();

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Packaging movement: {packagingMovement}",
                    e.Message, JsonSerializer.Serialize(packagingMovement));
            }

            return null;

        }

        private bool IsValidAmount(int currentTotal, AddPackagingMovementRequest packagingMovement)
        {
            return packagingMovement.Operation.Equals(Operation.Substract) ?
                (currentTotal - packagingMovement.Amount) >= 0 :
                packagingMovement.Amount >= 0;
        }
    }
    
}
