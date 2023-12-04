using FlightScheduleRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduleRepository.Repos
{
    public interface IFlightScheduleRepo
    {
        Task<List<FlightSchedule>> GetAllSchedules();
        Task<FlightSchedule> GetSchedule(string fno, DateTime trdate);
        Task<List<FlightSchedule>> GetSchedulesByFlight(string fno);
        Task<List<FlightSchedule>> GetSchedulesByDate(DateTime trdate);
        Task InsertSchedule(FlightSchedule schedule);
        Task UpdateSchedule(string fno, DateTime trdate, FlightSchedule schedule);
        Task DeleteSchedule(string fno, DateTime trdate);
        Task InsertFlight(Flight flight);
    }
}
