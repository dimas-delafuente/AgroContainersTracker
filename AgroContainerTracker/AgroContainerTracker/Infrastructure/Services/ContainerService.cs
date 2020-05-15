using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Core.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class ContainerService : IContainerService
    {

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;


        public ContainerService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Container> GetContainerAsync(int id)
        {
            if (id <= 0)
                throw new Exception();

            try
            {
                ContainerEntity container = await _context.Containers.FindAsync(id).ConfigureAwait(false);
                return _mapper.Map<Container>(container);
                //return Mocks.Mocks.Containers.Find(x => x.ContainerId == id);

            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<IEnumerable<Container>> GetContainersAsync()
        {
            try
            {
                var containers = await _context.Containers.ToListAsync().ConfigureAwait(false);
                return _mapper.Map<IEnumerable<Container>>(containers);
                //return Mocks.Mocks.Containers;

            }
            catch (Exception e) 
            {
                throw;
            }

        }
        public async Task AddContainerAsync(AddContainerRequest container)
        {
            if (container is null)
                throw new Exception();
            try
            {
                var result = await _context.Containers.AddAsync(new ContainerEntity
                {
                    ContainerId = container.ContainerId,
                    Size = container.Size
                });

                if (result.State == EntityState.Added)
                    _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
