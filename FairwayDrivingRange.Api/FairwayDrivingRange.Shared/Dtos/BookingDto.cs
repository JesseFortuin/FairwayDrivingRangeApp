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

        public DateTime dateBooked { get; set; }

        public int lane { get; set; }

        public int customerId { get; set; }
    }
}
