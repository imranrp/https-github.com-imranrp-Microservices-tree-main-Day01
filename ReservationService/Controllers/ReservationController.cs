using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationRepository.Models;
using ReservationRepository.Repos;

namespace ReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        IReservationMasterRepo masterRepo;
        IReservationDetailRepo detailRepo;
        public ReservationController(IReservationMasterRepo reservationMasterRepo, IReservationDetailRepo reservationDetailRepo)
        {
            masterRepo = reservationMasterRepo;
            detailRepo = reservationDetailRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<ReservationMaster> masters = await masterRepo.GetAll();
            return Ok(masters);
        }
        [HttpGet("{pnr}")]
        public async Task<ActionResult> GetOne(string pnr)
        {
            try
            {
                ReservationMaster master = await masterRepo.GetReservationMaster(pnr);
                return Ok(master);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByFlight/{fno}")]
        public async Task<ActionResult> GetByFlight(string fno)
        {
            try
            {
                List<ReservationMaster> masters = await masterRepo.GetReservationsByFlight(fno);
                return Ok(masters);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByDate/{trdate}")]
        public async Task<ActionResult> GetByDate(DateTime trdate)
        {
            try
            {
                List<ReservationMaster> masters = await masterRepo.GetReservationsByDate(trdate);
                return Ok(masters);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{fno}/{trdate}")]
        public async Task<ActionResult> GetByFlightAndDate(string fno, DateTime trdate)
        {
            try
            {
                List<ReservationMaster> masters = await masterRepo.GetReservationsByFlightAndDate(fno, trdate);
                return Ok(masters);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(ReservationMaster master)
        {
            await masterRepo.InsertReservationMaster(master);
            return Created($"api/reservation/{master.PNRNo}", master);
        }
        [HttpPost("FlightSchedule")]
        public async Task<ActionResult> InsertSchedule(FlightSchedule schedule)
        {
            await masterRepo.InsertFlightSchedule(schedule);
            return Created();
        }
        [HttpPut("{pnr}")]
        public async Task<ActionResult> Update(string pnr, ReservationMaster master)
        {
            await masterRepo.UpdateReservationMaster(pnr, master);
            return Ok(master);
        }
        [HttpDelete("{pnr}")]
        public async Task<ActionResult> Delete(string pnr)
        {
            await masterRepo.DeleteReservationMaster(pnr);
            return Ok();
        }
        [HttpGet("Passengers")]
        public async Task<ActionResult> GetPassengers()
        {
            List<ReservationDetail> details = await detailRepo.GetAll();
            return Ok(details);
        }
        [HttpGet("Passenger/{pnr}/{passno}")]
        public async Task<ActionResult> GetPassenger(string pnr, int passno)
        {
            try
            {
                ReservationDetail detail = await detailRepo.GetReservationDetail(pnr, passno);
                return Ok(detail);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("PassengersByPNR/{pnr}")]
        public async Task<ActionResult> GetByPNR(string pnr)
        {
            List<ReservationDetail> details = await detailRepo.GetReservationDetailsByPNR(pnr);
            return Ok(details);
        }
        [HttpPost("Passenger")]
        public async Task<ActionResult> InsertPassenger(ReservationDetail detail)
        {
            await detailRepo.InsertReservationDetail(detail);
            return Created($"api/reservation/{detail.PNRNo}/{detail.PassengerNo}", detail);
        }
        [HttpPut("{pnr}/{passno}")]
        public async Task<ActionResult> UpdatePassenger(string pnr, int passno, ReservationDetail detail)
        {
            await detailRepo.UpdateReservationDetail(pnr, passno, detail);
            return Ok(detail);
        }
        [HttpDelete("{pnr}/{passno}")]
        public async Task<ActionResult> DeletePassenger(string pnr, int passno)
        {
            await detailRepo.DeleteReservationDetail(pnr, passno);
            return Ok();
        }
    }
}
