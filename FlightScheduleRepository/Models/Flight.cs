using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduleRepository.Models
{
    [Table("Flight")]
    public partial class Flight
    {
        [Key]
        [StringLength(6)]
        public string FlightNo { get; set; }
        
        public virtual ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
    }

}
