using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PizzaMaster
{
    public class LocationsContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public LocationsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Locations.db");
        }
    }
}
