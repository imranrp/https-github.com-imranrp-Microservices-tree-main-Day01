using FlightScheduleRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduleRepository.Repos
{
    public class EFFlightScheduleRepo : IFlightScheduleRepo
    {
        WellsFargoFlightScheduleDBContext ctx = new WellsFargoFlightScheduleDBContext();
        public async Task DeleteSchedule(string fno, DateTime trdate)
        {
            FlightSchedule fs2del = await GetSchedule(fno, trdate);
            ctx.FlightSchedules.Remove(fs2del);
            await ctx.SaveChangesAsync();
        }
        public async Task<List<FlightSchedule>> GetAllSchedules()
        {
            List<FlightSchedule> schedules = await ctx.FlightSchedules.ToListAsync();
            return schedules;
        }
        public async Task<FlightSchedule> GetSchedule(string fno, DateTime trdate)
        {
            try
            {
                FlightSchedule schedule = await (from fs in ctx.FlightSchedules where fs.FlightNo == fno && fs.TravelDate == trdate select fs).FirstAsync();
                return schedule;
            }
            catch (Exception)
            {
                throw new Exception("The flight is not scheduled on this date");
            }
        }
        public async Task<List<FlightSchedule>> GetSchedulesByDate(DateTime trdate)
        {
            List<FlightSchedule> schedules = await (from fs in ctx.FlightSchedules where fs.TravelDate == trdate select fs).ToListAsync();
            if (schedules.Count > 0)
                return schedules;
            else
                throw new Exception("No flights scheduled on this date");
        }
        public async Task<List<FlightSchedule>> GetSchedulesByFlight(string fno)
        {
            List<FlightSchedule> schedules = await (from fs in ctx.FlightSchedules where fs.FlightNo == fno select fs).ToListAsync();
            if (schedules.Count > 0)
                return schedules;
            else
                throw new Exception("This flight is not yet scheduled");
        }

        public async Task InsertFlight(Flight flight)
        {
            await ctx.Flights.AddAsync(flight);
            await ctx.SaveChangesAsync();
        }

        public async Task InsertSchedule(FlightSchedule schedule)
        {
            await ctx.FlightSchedules.AddAsync(schedule);
            await ctx.SaveChangesAsync();
        }
        public async Task UpdateSchedule(string fno, DateTime trdate, FlightSchedule schedule)
        {
            FlightSchedule fs2edit = await GetSchedule(fno, trdate);
            fs2edit.DepartTime = schedule.DepartTime;
            fs2edit.ArriveTime = schedule.ArriveTime;
            await ctx.SaveChangesAsync();
        }
    }
}
