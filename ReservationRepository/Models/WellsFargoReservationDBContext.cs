using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationRepository.Models
{
    public class WellsFargoReservationDBContext : DbContext
    {
        public WellsFargoReservationDBContext()
        {
            
        }
        public WellsFargoReservationDBContext(DbContextOptions<WellsFargoReservationDBContext> options) : base(options)
        {
            
        }
        public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }

        public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }

        public virtual DbSet<ReservationMaster> ReservationMasters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=WellsFargoReservationDB; integrated security=true");
    }
}
