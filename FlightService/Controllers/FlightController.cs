using FlightRepository.Models;
using FlightRepository.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;

namespace FlightService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        IFlightRepo flightRepo;
        public FlightController(IFlightRepo repo)
        {
            flightRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Flight> flights = await flightRepo.GetAllFlights();
            return Ok(flights);
        }
        [HttpGet("{fno}")]
        public async Task<ActionResult> Get(string fno)
        {
            try
            {
                Flight flight = await flightRepo.GetFlight(fno);
                return Ok(flight);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "flightxchange", routingKey: integrationEvent, basicProperties: null, body: body);
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Flight flight)
        {
            await flightRepo.InsertFlight(flight);
            // Call FlightSchedule service synchronously
            /*HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5006/api/FlightSchedule/");
            await client.PostAsJsonAsync<Flight>("" + "Flight", flight);*/
            // Write the flight details to RabbitMQ Queue
            var eventData = JsonConvert.SerializeObject(new { FlightNo = flight.FlightNo });
            PublishToMessageQueue("flight.add", eventData);
            return Created($"api/flight/{flight.FlightNo}", flight);
        }
        [HttpPut("{fno}")]
        public async Task<ActionResult> Update(string fno, Flight flight)
        {
            await flightRepo.UpdateFlight(fno, flight);
            return Ok(flight);
        }
        [HttpDelete("{fno}")]
        public async Task<ActionResult> Delete(string fno)
        {
            await flightRepo.DeleteFlight(fno);
            return Ok();
        }
    }
}
