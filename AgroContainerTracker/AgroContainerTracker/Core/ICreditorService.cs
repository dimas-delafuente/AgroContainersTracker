using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Core.Services
{
    public interface ICreditorService
    {
        Task<IEnumerable<Creditor>> GetAllAsync();

        Task<Creditor> GetByIdAsync(int id);

        Task AddAsync(AddCreditorRequest creditor);
    }
}
