using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Entities
{
    public class LicnostContext : DbContext
    {

        public LicnostContext(DbContextOptions<LicnostContext> options) : base(options) 
        {
            
        }

        public DbSet<Licnost> Licnosti { get; set; }

    }
}
