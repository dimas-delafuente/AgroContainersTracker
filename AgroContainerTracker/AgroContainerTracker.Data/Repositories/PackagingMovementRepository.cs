using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Entities;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgroContainerTracker.Data.Repositories
{
    internal class PackagingMovementRepository : IPackagingMovementRepository
    {
        private readonly ApplicationContext _context;

        public PackagingMovementRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<PackagingMovement> AddAsync(PackagingMovement packagingMovement, CancellationToken cancellationToken)
        {
            Ensure.NotNull(packagingMovement, nameof(packagingMovement));

            try
            {
                var packaging = await _context.Packagings
                    .FindAsync(packagingMovement.PackagingId, cancellationToken)
                    .ConfigureAwait(false);

                if (packaging != null)
                {
                    packaging.Total = packagingMovement.Operation.Equals(Operation.Add) ?
                        packaging.Total + packagingMovement.Amount :
                        packaging.Total - packagingMovement.Amount;

                    packagingMovement.Total = packaging.Total;

                    var added = await _context.PackagingMovements.AddAsync(packagingMovement, cancellationToken).ConfigureAwait(false);
                    if (added.State.Equals(EntityState.Added))
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return packagingMovement;
                    }
                }

                _context.DetachAll();

            }
            catch
            {
                _context.DetachAll();
                throw;
            }

            return null;
        }

        public async Task<IEnumerable<PackagingMovement>> GetByCustomerIdAsync(int customerId, DateTime initDate, DateTime endDate, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Packagings
                    .AsNoTracking()
                    .Where(x => x.CustomerId.Equals(customerId))
                    .SelectMany(x => x.PackagingMovements.Where(pm => pm.Created >= initDate && pm.Created <= endDate)
                        .Select(pm => new PackagingMovement
                        {
                            PackagingMovementId = pm.PackagingMovementId,
                            Customer = pm.Customer,
                            Packaging = pm.Packaging,
                            Created = pm.Created,
                            Operation = pm.Operation,
                            Amount = pm.Amount,
                            Total = pm.Total
                        }))
                    .OrderBy(x => x.Created)
                    .ToListAsync()
                    .ConfigureAwait(false);

            }
            catch
            {
                _context.DetachAll();
                throw;
            }

            return Enumerable.Empty<PackagingMovement>();
        }
    }
}
