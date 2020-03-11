using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure
{
    public class PalotService : IPalotService
    {

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public PalotService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public Task AddPalotAsync(AddPalotRequest palot)
        {
            throw new NotImplementedException();
        }

        public async Task<Palot> GetPalotAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception();

            try
            {
                PalotEntity palot = await _context.Palots.FindAsync(id).ConfigureAwait(false);
                return _mapper.Map<Palot>(palot);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Palot>> GetPalotsAsync()
        {
            try
            {
                var palots = await _context.Palots.ToListAsync().ConfigureAwait(false);
                return _mapper.Map<IEnumerable<Palot>>(palots);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
