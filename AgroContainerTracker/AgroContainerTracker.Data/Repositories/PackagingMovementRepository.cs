using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain;
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
                // TODO: Convert to domain event
                var packaging = await _context.Packagings
                    .FindAsync(new object[] { packagingMovement.Packaging.Id }, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (packaging != null)
                {
                    packaging.Total = packagingMovement.Operation.Equals(PackagingMovementOperation.Add) ?
                        packaging.Total + packagingMovement.Quantity :
                        packaging.Total - packagingMovement.Quantity;

                    packagingMovement.Total = packaging.Total;

                    var added = await _context.PackagingMovements.AddAsync(packagingMovement, cancellationToken).ConfigureAwait(false);
                    if (added.State.Equals(EntityState.Added))
                    {
                        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
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
                return await _context.PackagingMovements
                    .AsNoTracking()
                    .Include(x => x.Packaging)
                        .ThenInclude(x => x.Owner)
                    .Include(x => x.Receiver)
                    .Where(x => x.Packaging.Owner.Company.Id == customerId)
                    .OrderBy(x => x.Created)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch
            {
                _context.DetachAll();
                throw;
            }
        }
    }
}
