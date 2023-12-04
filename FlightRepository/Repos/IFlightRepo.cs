using FlightRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRepository.Repos
{
    public interface IFlightRepo
    {
        Task<List<Flight>> GetAllFlights();
        Task<Flight> GetFlight(string fno);
        Task InsertFlight(Flight flight);
        Task UpdateFlight(string fno, Flight flight);
        Task DeleteFlight(string fno);
    }
}
