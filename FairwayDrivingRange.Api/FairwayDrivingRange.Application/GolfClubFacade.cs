using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Application
{
    public class GolfClubFacade : IGolfClubFacade
    {
        private readonly IRepository<GolfClub> repository;

        public GolfClubFacade(IRepository<GolfClub> repository)
        {
            this.repository = repository;
        }

        public ApiResponseDto<bool> AddGolfClub(AddGolfClubDto golfClubDto)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<bool> DeleteGolfClub(int golfClubId)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<GolfClubDto> GetGolfClubById(int golfClubId)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<IEnumerable<GolfClubDto>> GetGolfClubs()
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<bool> UpdateGolfClub(int golfClubId, AddGolfClubDto golfClubDto)
        {
            throw new NotImplementedException();
        }
    }
}
