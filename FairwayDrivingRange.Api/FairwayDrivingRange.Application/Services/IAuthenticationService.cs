using FairwayDrivingRange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Application.Services
{
    public interface IAuthenticationService
    {
        public string GenerateToken(string adminName);
    }
}
