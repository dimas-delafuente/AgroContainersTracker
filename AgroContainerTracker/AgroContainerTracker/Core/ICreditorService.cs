using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Core.Services
{
    public interface ICreditorService
    {
        Task<List<Creditor>> GetAllAsync();

        Task<Creditor> GetByIdAsync(int creditorId);

        Task AddAsync(AddCreditorRequest creditor);

        Task<bool> DeleteAsync(int creditorId);

        Task<bool> UpdateAsync(Creditor creditor);

    }
}
