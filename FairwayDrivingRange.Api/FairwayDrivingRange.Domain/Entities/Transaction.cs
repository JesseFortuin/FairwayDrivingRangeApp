using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public CustomerInformation CustomerInformation { get; set; }

        public Guid CustomerId { get; set; }

        public double ClubPrice { get; set; }

        public double BookingPrice { get; set; }

        public double Total { get; set; }
    }
}
