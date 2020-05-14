using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Rates;

namespace AgroContainerTracker.Core.Services
{
    public interface IRateService
    {
        Task<List<Rate>> GetAllAsync();
        Task<Rate> GetByIdAsync(int rateId);
        Task<RateDetails> GetDetailsByIdAsync(int rateId);
        Task<bool> DeleteAsync(int rateId);
        Task<bool> UpdateAsync(Rate rate);
        Task<Rate> AddAsync(AddRateRequest rate);

    }
}
