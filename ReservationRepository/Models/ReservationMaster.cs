using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationRepository.Models
{
    [Table("ReservationMaster")]
    public partial class ReservationMaster
    {
        [Key]
        [StringLength(6)]
        public string PNRNo { get; set; }
        [StringLength(6)]
        public string? FlightNo { get; set; }

        public DateTime? TravelDate { get; set; }

        public int? NoOfPassengers { get; set; }

        [ForeignKey("FlightNo, TravelDate")]
        public virtual FlightSchedule? FlightSchedule { get; set; }

        public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();
    }
}
