using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPink.Models
{
    public class Notification
    {
        public string EmailAddress { get; set; }
        public string Title { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
