using ReservationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationRepository.Repos
{
    public interface IReservationDetailRepo
    {
        Task<List<ReservationDetail>> GetAll();
        Task<ReservationDetail> GetReservationDetail(string pnr, int passNo);
        Task<List<ReservationDetail>> GetReservationDetailsByPNR(string pnr);
        Task InsertReservationDetail(ReservationDetail detail);
        Task UpdateReservationDetail(string pnr, int passNo, ReservationDetail detail);
        Task DeleteReservationDetail(string pnr, int passNo);
    }
}
