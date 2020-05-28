using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Packagings;
using AgroContainerTracker.Domain.Reports;

namespace AgroContainerTracker.Core.Services
{
    public interface IPackagingService
    {
        public Task<List<Packaging>> GetAllAsync();
        Task<Packaging> GetByIdAsync(int packagingId);
        Task<Packaging> FindAsync(int? packagingId);
        Task<bool> DeleteAsync(int packagingId);
        Task<bool> UpdateAsync(Packaging packaging);
        Task<Packaging> AddAsync(AddPackagingRequest packaging);
        Task<bool> ExistsAsync(string packagingCode);
        Task<Packaging> AddPackagingMovementAsync(AddPackagingMovementRequest packagingMovement);

        Task<IEnumerable<PackagingMovement>> GetCustomerPackagingMovementsAsync(int customerId, DateTime initDate, DateTime endDate);
    }
}
