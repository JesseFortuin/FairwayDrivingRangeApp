using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime DateBooked { get; set; }

        public int Lane { get; set; }

        public CustomerInformation Customer { get; set; }

        public Guid CustomerId { get; set; }    

        public List<GolfClub> clubs { get; set; } 
    }
}
