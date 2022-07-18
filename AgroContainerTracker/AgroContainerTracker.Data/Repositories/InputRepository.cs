using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Shared;
using Microsoft.EntityFrameworkCore;
using System;

namespace AgroContainerTracker.Data.Repositories
{
    public class InputRepository : IInputRepository
    {
        private readonly ApplicationContext _context;

        public InputRepository(ApplicationContext context)
        {
            Ensure.NotNull(context, nameof(context));
            _context = context;
        }

        public async Task<Input> AddAsync(Input Input, CancellationToken cancellationToken)
        {
            Ensure.NotNull(Input, nameof(Input));

            try
            {
                var addResponse = await _context.Inputs.AddAsync(Input, cancellationToken).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
                    return created ? addResponse.Entity : null;
                }

                return null;
            }
            catch
            {
                _context.DetachAll();
                throw;
            }
        }

        public async Task<bool> CloseEntryAsync(int campaignId, int InputNumber, CancellationToken cancellationToken)
        {
            Ensure.Positive(campaignId, nameof(campaignId));
            Ensure.Positive(InputNumber, nameof(InputNumber));

            try
            {
                // TODO: Change to update
                var entity = await _context.Inputs
                        .FirstOrDefaultAsync(x => x.Campaign.Id.Equals(campaignId) && x.Id.Equals(InputNumber), cancellationToken)
                        .ConfigureAwait(false);
                if (entity != null)
                {
                    entity.Closed = true;
                    entity.CreatedDate = DateTime.Now;
                    return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
                }
            }
            catch
            {
                _context.DetachAll();
                throw;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int campaignId, int InputNumber, CancellationToken cancellationToken)
        {
            Ensure.Positive(campaignId, nameof(campaignId));
            Ensure.Positive(InputNumber, nameof(InputNumber));

            try
            {
                var Input = await _context.Inputs.FindAsync(new object[] { campaignId, InputNumber }, cancellationToken: cancellationToken).ConfigureAwait(false);

                if (Input != null)
                {
                    var removedEntity = _context.Inputs.Remove(Input);
                    if (removedEntity?.Entity != null && removedEntity.State.Equals(EntityState.Deleted))
                    {
                        int deleted = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        return deleted > 0;
                    }
                }
            }
            catch
            {
                _context.DetachAll();
                throw;
            }

            return false;
        }

        public async Task<bool> ExistsAsync(int campaignId, int InputNumber, CancellationToken cancellationToken)
        {
            Ensure.Positive(campaignId, nameof(campaignId));
            Ensure.Positive(InputNumber, nameof(InputNumber));

            try
            {
                return await _context.Inputs
                    .Include(x => x.Campaign)
                    .FirstOrDefaultAsync(x => x.Campaign.Id.Equals(campaignId) && x.Id.Equals(InputNumber), cancellationToken)
                    .ConfigureAwait(false) is not null;
            }
            catch
            {
                _context.DetachAll();
                throw;
            }
        }

        public async Task<Input> GetAsync(int campaignId, int InputNumber, CancellationToken cancellationToken)
        {
            Ensure.Positive(campaignId, nameof(campaignId));
            Ensure.Positive(InputNumber, nameof(InputNumber));

            try
            {
                return await _context.Inputs
                    .AsNoTracking()
                    .Include(x => x.Campaign)
                    // .Include(x => x.Buyer)
                    // .Include(x => x.Payer)
                    .Include(x => x.Sellers)
                    .Include(x => x.Weighings)
                    .FirstOrDefaultAsync(x => x.Campaign.Id.Equals(campaignId) && x.Id.Equals(InputNumber), cancellationToken)
                    .ConfigureAwait(false);
            }
            catch
            {
                _context.DetachAll();
                throw;
            }
        }

        public async Task<List<Input>> GetByCampaignIdAsync(int campaignId, CancellationToken cancellationToken)
        {
            Ensure.Positive(campaignId, nameof(campaignId));

            try
            {
                return await _context.Inputs
                    .AsNoTracking()
                    .Include(x => x.Campaign)
                    // .Include(x => x.Buyer)
                    // .Include(x => x.Payer)
                    .Include(x => x.Sellers)
                    .Include(x => x.Weighings)
                    .Where(x => x.Campaign.Id.Equals(campaignId))
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch
            {
                _context.DetachAll();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Input Input, CancellationToken cancellationToken)
        {
            Ensure.NotNull(Input, nameof(Input));

            try
            {
                var entity = await _context.Inputs
                    .FindAsync(new object[] { new { CampaignId = Input.Campaign.Id, Input.Id } }, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    // TODO: Check update
                    // entity.Buyer = Input.Buyer;
                    entity.CreatedDate = Input.CreatedDate;
                    entity.HasHail = Input.HasHail;
                    entity.HasPlague = Input.HasPlague;
                    entity.HasOutput = Input.HasOutput;
                    entity.HasSecondPasses = Input.HasSecondPasses;
                    entity.Observations = Input.Observations;
                    // entity.Payer = Input.Payer;
                    entity.Sellers = Input.Sellers;

                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    return true;
                }
            }
            catch
            {
                _context.DetachAll();
                throw;
            }

            return false;
        }
    }
}