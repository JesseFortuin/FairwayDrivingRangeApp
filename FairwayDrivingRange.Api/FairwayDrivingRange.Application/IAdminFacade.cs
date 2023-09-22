using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Application
{
    public interface IAdminFacade
    {
        public ApiResponseDto<bool> RegisterAdmin(AdminDto adminDto);

        public ApiResponseDto<string> Login(AdminDto adminDto);
    }
}
