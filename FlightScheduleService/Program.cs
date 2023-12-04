
using FlightScheduleRepository.Models;
using FlightScheduleRepository.Repos;
using FlightScheduleService.Controllers;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace FlightScheduleService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ListenForIntegrationEvents();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IFlightScheduleRepo, EFFlightScheduleRepo>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ListenForIntegrationEvents()
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                if (type == "flight.add")
                {
                    Flight flight = new Flight { FlightNo = data["FlightNo"].Value<string>() };
                    IFlightScheduleRepo fsRepo = new EFFlightScheduleRepo();
                    fsRepo.InsertFlight(flight);
                }
            };
            channel.BasicConsume(queue: "flightqueue", autoAck: true, consumer: consumer);
        }
    }
}
