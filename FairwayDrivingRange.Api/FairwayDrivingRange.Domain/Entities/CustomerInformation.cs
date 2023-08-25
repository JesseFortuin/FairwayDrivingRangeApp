using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Domain.Entities
{
    public class CustomerInformation
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsPaid { get; set; }

        public Booking? Booking { get; set; }

        public Transaction? Transaction { get; set; }
    }
}
