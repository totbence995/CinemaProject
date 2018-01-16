using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPink.Models
{
    public class Seat
    {
        public int ID { get; set; }
        public int RoomID { get; set; }

        public Room Room { get; set; }
    }
}
