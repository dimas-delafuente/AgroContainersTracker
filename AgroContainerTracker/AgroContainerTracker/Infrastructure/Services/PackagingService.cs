using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Packagings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Operation = AgroContainerTracker.Domain.Packagings.Operation;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class PackagingService : IPackagingService
    {

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public PackagingService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(AddPackagingRequest packaging)
        {
            try
            {
                if (packaging == null)
                    throw new ArgumentNullException();

                PackagingEntity entity = _mapper.Map<PackagingEntity>(packaging);

                var addResponse = await _context.Packagings.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                    await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _context.DetachAll();
                throw;
            }
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
            catch (Exception)
            {
                _context.DetachAll();
                return false;
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
            catch (Exception)
            {
                _context.DetachAll();
                return new List<Packaging>();
            }
        }

        public async Task<Packaging> GetByIdAsync(int packagingId)
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
            catch (Exception)
            {
                _context.DetachAll();
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
