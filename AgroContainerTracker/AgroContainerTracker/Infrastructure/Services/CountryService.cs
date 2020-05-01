using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CountryService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            IEnumerable<CountryEntity> entities = await _context.Countries
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Country>>(entities);
        }
    }
}
