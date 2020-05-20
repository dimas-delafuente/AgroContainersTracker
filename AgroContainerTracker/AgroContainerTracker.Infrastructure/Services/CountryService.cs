using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly ILogger<CountryService> _logger;

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CountryService(ApplicationContext context, IMapper mapper, ILogger<CountryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            try
            {
                IEnumerable<CountryEntity> entities = await _context.Countries
                                .AsNoTracking()
                                .OrderBy(x => x.Name)
                                .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<IEnumerable<Country>>(entities);

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all countries.", e.Message);
                return new List<Country>();
            }

        }
    }
}
