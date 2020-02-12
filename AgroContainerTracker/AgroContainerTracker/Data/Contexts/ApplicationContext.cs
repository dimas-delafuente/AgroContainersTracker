using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroContainerTracker.Data.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Container> Containers { get; set; }
    }
}
