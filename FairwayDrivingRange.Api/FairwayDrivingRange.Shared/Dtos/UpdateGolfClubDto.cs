using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Shared.Dtos
{
    public class UpdateGolfClubDto
    {
        public int serialNumber { get; set; }

        public int? bookingId { get; set; }

        public bool isAvailable { get; set; }
    }
}
