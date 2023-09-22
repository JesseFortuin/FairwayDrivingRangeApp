using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Shared.Dtos
{
    public class TransactionDto
    {
        public int id { get; set; }

        public int customerId { get; set; }

        public double? clubPrice { get; set; }

        public double bookingPrice { get; set; }

        public double total { get; set; }
    }
}
