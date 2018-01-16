using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPink.Models
{
    public class Reservation
    {

        public int ID { get; set; }
        public int SeatID { get; set; }

        public int ProjectionID { get; set; }

        public Seat Seat { get; set; }

        public Projection Projection { get; set; }

    }
}
