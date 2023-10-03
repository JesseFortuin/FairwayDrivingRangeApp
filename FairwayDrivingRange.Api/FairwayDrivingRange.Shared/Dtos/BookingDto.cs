using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Shared.Dtos
{
    public class BookingDto
    {
        public int id { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public int customerId { get; set; }
    }
}
