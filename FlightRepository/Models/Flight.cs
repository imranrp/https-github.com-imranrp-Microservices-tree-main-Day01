using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRepository.Models
{
    [Table("Flight")]
    public partial class Flight
    {
        [Key]
        [StringLength(6)]
        public string FlightNo { get; set; }
        [StringLength(20)]
        public string FromCity { get; set; }
        [StringLength(20)]
        public string ToCity { get; set; }
        public int? TotalSeats { get; set; }
    }
}
