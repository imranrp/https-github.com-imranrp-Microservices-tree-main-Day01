using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationRepository.Models;

namespace ReservationRepository.Repos
{
    public interface IReservationMasterRepo
    {
        Task<List<ReservationMaster>> GetAll();
        Task<ReservationMaster> GetReservationMaster(string pnr);
        Task<List<ReservationMaster>> GetReservationsByFlight(string fno);
        Task<List<ReservationMaster>> GetReservationsByDate(DateTime trdate);
        Task<List<ReservationMaster>> GetReservationsByFlightAndDate(string fno, DateTime trdate);
        Task InsertReservationMaster(ReservationMaster master);
        Task UpdateReservationMaster(string pnr, ReservationMaster master);
        Task DeleteReservationMaster(string pnr);
        Task InsertFlightSchedule(FlightSchedule schedule);
    }
}
