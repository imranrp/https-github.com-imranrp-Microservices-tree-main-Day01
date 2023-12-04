using FlightRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRepository.Repos
{
    public class EFFlightRepo : IFlightRepo
    {
        WellsFargoFlightDBContext ctx = new WellsFargoFlightDBContext();
        public async Task DeleteFlight(string fno)
        {
            Flight flt2del = await GetFlight(fno);
            ctx.Flights.Remove(flt2del);
            await ctx.SaveChangesAsync();
        }
        public async Task<List<Flight>> GetAllFlights()
        {
            List<Flight> flights = await ctx.Flights.ToListAsync();
            return flights;
        }
        public async Task<Flight> GetFlight(string fno)
        {
            try
            {
                Flight flight = await (from fl in ctx.Flights where fl.FlightNo == fno select fl).FirstAsync();
                return flight;
            }
            catch (Exception)
            {
                throw new Exception("No such flight no.");
            }
        }
        public async Task InsertFlight(Flight flight)
        {
            await ctx.Flights.AddAsync(flight);
            await ctx.SaveChangesAsync();
        }
        public async Task UpdateFlight(string fno, Flight flight)
        {
            Flight flt2edit = await GetFlight(fno);
            flt2edit.FromCity = flight.FromCity;
            flt2edit.ToCity = flight.ToCity;
            flt2edit.TotalSeats = flight.TotalSeats;
            await ctx.SaveChangesAsync();
        }
    }
}
