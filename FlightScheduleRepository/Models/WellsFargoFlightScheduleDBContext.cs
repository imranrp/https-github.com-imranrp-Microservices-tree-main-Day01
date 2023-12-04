using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduleRepository.Models
{
    public class WellsFargoFlightScheduleDBContext : DbContext
    {
        public WellsFargoFlightScheduleDBContext()
        {
            
        }
        public WellsFargoFlightScheduleDBContext(DbContextOptions<WellsFargoFlightScheduleDBContext> options):base(options)
        {
            
        }
        public virtual DbSet<Flight> Flights { get; set; }

        public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=WellsFargoFlightScheduleDB; integrated security=true");
    }
}
