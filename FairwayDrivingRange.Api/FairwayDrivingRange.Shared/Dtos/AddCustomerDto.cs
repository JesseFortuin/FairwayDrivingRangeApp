using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Shared.Dtos
{
    public class AddCustomerDto
    {
        public string name {  get; set; }

        public string email { get; set; }

        public bool isPaid { get; set; }

        public long phone {  get; set; }
    }
}
