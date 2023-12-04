using Microsoft.EntityFrameworkCore;
using ReservationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationRepository.Repos
{
    public class EFReservationDetailRepo : IReservationDetailRepo
    {
        WellsFargoReservationDBContext ctx = new WellsFargoReservationDBContext();
        public async Task DeleteReservationDetail(string pnr, int passNo)
        {
            ReservationDetail rd2del = await GetReservationDetail(pnr, passNo);
            ctx.ReservationDetails.Remove(rd2del);
            await ctx.SaveChangesAsync();
            EFReservationMasterRepo masterRepo = new EFReservationMasterRepo();
            ReservationMaster master = await masterRepo.GetReservationMaster(pnr);
            master.NoOfPassengers--;
            await masterRepo.UpdateReservationMaster(master.PNRNo, master);
        }
        public async Task<List<ReservationDetail>> GetAll()
        {
            List<ReservationDetail> details = await ctx.ReservationDetails.ToListAsync();
            return details;
        }
        public async Task<ReservationDetail> GetReservationDetail(string pnr, int passNo)
        {
            try
            {
                ReservationDetail detail = await (from rd in ctx.ReservationDetails where rd.PNRNo == pnr && rd.PassengerNo == passNo select rd).FirstAsync();
                return detail;
            }
            catch (Exception)
            {
                throw new Exception("No such PNR no. or passenger no.");
            }
        }
        public async Task<List<ReservationDetail>> GetReservationDetailsByPNR(string pnr)
        {
            List<ReservationDetail> details = await (from rd in ctx.ReservationDetails where rd.PNRNo == pnr select rd).ToListAsync();
            //if (details.Count > 0)
            return details;
            //else
            //throw new Exception("No passengers for this PNR no.");
        }
        public async Task InsertReservationDetail(ReservationDetail detail)
        {
            await ctx.ReservationDetails.AddAsync(detail);
            await ctx.SaveChangesAsync();
            EFReservationMasterRepo masterRepo = new EFReservationMasterRepo();
            ReservationMaster master = await masterRepo.GetReservationMaster(detail.PNRNo);
            master.NoOfPassengers++;
            await masterRepo.UpdateReservationMaster(master.PNRNo, master);
        }
        public async Task UpdateReservationDetail(string pnr, int passNo, ReservationDetail detail)
        {
            ReservationDetail rd2edit = await GetReservationDetail(pnr, passNo);
            rd2edit.FirstName = detail.FirstName;
            rd2edit.LastName = detail.LastName;
            rd2edit.Gender = detail.Gender;
            rd2edit.Age = detail.Age;
            await ctx.SaveChangesAsync();
        }
    }
}
