using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Domain.Entities
{
    public class GolfClub : IEntity
    {
        public int Id { get; set; }

        public int SerialNumber { get; set; }

        public int? BookingId { get; set; }

        public Booking Booking { get; set; }

        public bool IsAvailable { get; set; }
    }
}
