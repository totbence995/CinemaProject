using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPink.Models
{
    public class SeatDTO
    {
        public SeatDTO(int iD, bool reserved=false)
        {
            ID = iD;
            Reserved = reserved;
        }

        public int ID { get; set; }
        public bool Reserved { get; set; }
    }
}
