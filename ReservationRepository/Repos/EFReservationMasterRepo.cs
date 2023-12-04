using Microsoft.EntityFrameworkCore;
using ReservationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationRepository.Repos
{
    public class EFReservationMasterRepo : IReservationMasterRepo
    {
        WellsFargoReservationDBContext ctx = new WellsFargoReservationDBContext();
        public async Task DeleteReservationMaster(string pnr)
        {
            ReservationMaster rm2del = await GetReservationMaster(pnr);
            ctx.ReservationMasters.Remove(rm2del);
            await ctx.SaveChangesAsync();
        }
        public async Task<List<ReservationMaster>> GetAll()
        {
            List<ReservationMaster> masters = await ctx.ReservationMasters.ToListAsync();
            return masters;
        }
        public async Task<ReservationMaster> GetReservationMaster(string pnr)
        {
            try
            {
                ReservationMaster master = await (from rm in ctx.ReservationMasters where rm.PNRNo == pnr select rm).FirstAsync();
                return master;
            }
            catch (Exception)
            {
                throw new Exception("No such PNR no.");
            }
        }
        public async Task<List<ReservationMaster>> GetReservationsByDate(DateTime trdate)
        {
            List<ReservationMaster> masters = await (from rm in ctx.ReservationMasters where rm.TravelDate == trdate select rm).ToListAsync();
            if (masters.Count > 0)
                return masters;
            else
                throw new Exception("No reservations on this date");
        }
        public async Task<List<ReservationMaster>> GetReservationsByFlight(string fno)
        {
            List<ReservationMaster> masters = await (from rm in ctx.ReservationMasters where rm.FlightNo == fno select rm).ToListAsync();
            if (masters.Count > 0)
                return masters;
            else
                throw new Exception("No reservations for this flight");
        }
        public async Task<List<ReservationMaster>> GetReservationsByFlightAndDate(string fno, DateTime trdate)
        {
            List<ReservationMaster> masters = await (from rm in ctx.ReservationMasters where rm.FlightNo == fno && rm.TravelDate == trdate select rm).ToListAsync();
            if (masters.Count > 0)
                return masters;
            else
                throw new Exception("No reservations for this flight on this date");
        }

        public async Task InsertFlightSchedule(FlightSchedule schedule)
        {
            await ctx.FlightSchedules.AddAsync(schedule);
            await ctx.SaveChangesAsync();
        }

        public async Task InsertReservationMaster(ReservationMaster master)
        {
            await ctx.ReservationMasters.AddAsync(master);
            await ctx.SaveChangesAsync();
        }
        public async Task UpdateReservationMaster(string pnr, ReservationMaster master)
        {
            ReservationMaster rm2edit = await GetReservationMaster(pnr);
            rm2edit.FlightNo = master.FlightNo;
            rm2edit.TravelDate = master.TravelDate;
            rm2edit.NoOfPassengers = master.NoOfPassengers;
            await ctx.SaveChangesAsync();
        }
    }
}
