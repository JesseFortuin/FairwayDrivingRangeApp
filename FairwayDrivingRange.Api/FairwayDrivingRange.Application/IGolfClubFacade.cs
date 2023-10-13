using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public interface IGolfClubFacade
    {
        public ApiResponseDto<bool> AddGolfClub(AddGolfClubDto golfClubDto);

        public ApiResponseDto<bool> AddAllGolfClubs(params AddGolfClubDto[] golfClubDtos);

        public ApiResponseDto<bool> UpdateGolfClub(int golfClubId, UpdateGolfClubDto golfClubDto);

        public ApiResponseDto<IEnumerable<GolfClubDto>> GetGolfClubs();

        public ApiResponseDto<GolfClubDto> GetGolfClubById(int golfClubId);

        public ApiResponseDto<bool> DeleteGolfClub(int golfClubId);
    }
}
