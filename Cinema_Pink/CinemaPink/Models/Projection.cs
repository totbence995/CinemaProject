using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPink.Models
{
    public class Projection
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int RoomID { get; set; }
        public int FilmID { get; set; }
        public Room Room { get; set; }

        public Film Film { get; set; }
       
    }
}
