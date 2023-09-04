using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Domain.Entities
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }

        public CustomerInformation CustomerInformation { get; set; }

        public int CustomerId { get; set; }

        public double ClubPrice { get; set; }

        public double BookingPrice { get; set; }

        public double Total { get; set; }
    }
}
