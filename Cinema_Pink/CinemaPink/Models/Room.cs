using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPink.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }


        public IEnumerable<Projection> Projections { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
      
    }
}
