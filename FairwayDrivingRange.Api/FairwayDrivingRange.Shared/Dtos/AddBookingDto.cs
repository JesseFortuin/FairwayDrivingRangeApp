using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Shared.Dtos
{
    public class AddBookingDto
    {
        public DateTime dateBooked { get; set; }

        public int lane { get; set; }

        public int customerId { get; set; }
    }
}
