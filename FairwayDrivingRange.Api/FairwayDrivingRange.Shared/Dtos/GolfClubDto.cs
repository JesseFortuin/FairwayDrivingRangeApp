using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Shared.Dtos
{
    public class GolfClubDto
    {
        public int id { get; set; }

        public int serialNumber { get; set; }

        public int? bookingId { get; set; }

        public bool isAvailable { get; set; }
    }
}
