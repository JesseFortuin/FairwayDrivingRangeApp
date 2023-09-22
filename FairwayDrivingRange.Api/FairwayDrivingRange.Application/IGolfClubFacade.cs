using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Application
{
    public interface IGolfClubFacade
    {
        public ApiResponseDto<bool> AddGolfClub(AddGolfClubDto golfClubDto);

        public ApiResponseDto<bool> UpdateGolfClub(int golfClubId, UpdateGolfClubDto golfClubDto);

        public ApiResponseDto<IEnumerable<GolfClubDto>> GetGolfClubs();

        public ApiResponseDto<GolfClubDto> GetGolfClubById(int golfClubId);

        public ApiResponseDto<bool> DeleteGolfClub(int golfClubId);
    }
}
