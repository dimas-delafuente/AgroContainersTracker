using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Packagings;

namespace AgroContainerTracker.Core.Services
{
    public interface IPackagingService
    {
        public Task<List<Packaging>> GetAllAsync();
        Task<Packaging> GetByIdAsync(int packagingId);
        Task<bool> DeleteAsync(int packagingId);
        Task<bool> UpdateAsync(Packaging packaging);
        Task<Packaging> AddAsync(AddPackagingRequest packaging);
        Task<Packaging> AddPackagingMovementAsync(AddPackagingMovementRequest packagingMovement);
    }
}
