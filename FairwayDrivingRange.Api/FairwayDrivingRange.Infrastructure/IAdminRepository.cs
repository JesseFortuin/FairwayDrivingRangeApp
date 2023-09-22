using FairwayDrivingRange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure
{
    public interface IAdminRepository
    {
        public Admin GetAdminByName(string name);
    }
}
