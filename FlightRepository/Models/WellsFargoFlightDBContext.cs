using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FlightRepository.Models
{
    public class WellsFargoFlightDBContext : DbContext
    {
        public WellsFargoFlightDBContext()
        {
            
        }
        public WellsFargoFlightDBContext(DbContextOptions<WellsFargoFlightDBContext> options) :base(options)
        {
            
        }
        public virtual DbSet<Flight> Flights { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=WellsFargoFlightDB; integrated security=true");

    }
}
