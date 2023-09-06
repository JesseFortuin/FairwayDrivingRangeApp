using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Domain.Entities
{
    public class CustomerInformation : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsPaid { get; set; }

        public List<Booking> Booking { get; set; } = new List<Booking>();

        public List<Transaction> Transaction { get; set; } = new List<Transaction>();
    }
}
