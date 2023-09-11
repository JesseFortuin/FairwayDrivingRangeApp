using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public class GolfClubFacade : IGolfClubFacade
    {
        private readonly IRepository<GolfClub> repository;
        private readonly IRepository<Booking> bookingRepository;
        private readonly IMapper mapper;

        public GolfClubFacade(IRepository<GolfClub> repository, 
                            IRepository<Booking> bookingRepository,
                                 IMapper mapper)
        {
            this.repository = repository;
            this.bookingRepository = bookingRepository;
            this.mapper = mapper;
        }

        public ApiResponseDto<bool> AddGolfClub(AddGolfClubDto golfClubDto)
        {
            if (golfClubDto.serialNumber <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Serial Number");
            };

            var golfClub = mapper.Map<GolfClub>(golfClubDto);

            var result = repository.Create(golfClub);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteGolfClub(int golfClubId)
        {
            if (golfClubId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Golf Club Id");
            };

            var golfClub = repository.GetById(golfClubId);

            if (golfClub == null)
            {
                return new ApiResponseDto<bool>("Golf Club Not Found");
            };

            var result = repository.Delete(golfClub);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<GolfClubDto> GetGolfClubById(int golfClubId)
        {
            if (golfClubId <= 0)
            {
                return new ApiResponseDto<GolfClubDto>("Invalid Golf Club Id");
            };

            var golfClub = repository.GetById(golfClubId);

            if (golfClub == null)
            {
                return new ApiResponseDto<GolfClubDto>("Golf Club Not Found");
            }

            var golfClubDto = mapper.Map<GolfClubDto>(golfClub);

            return new ApiResponseDto<GolfClubDto>(golfClubDto);
        }

        public ApiResponseDto<IEnumerable<GolfClubDto>> GetGolfClubs()
        {
            var golfClubs = repository.GetAll();

            var golfClubDtos = mapper.Map<IEnumerable<GolfClubDto>>(golfClubs);

            return new ApiResponseDto<IEnumerable<GolfClubDto>>(golfClubDtos);
        }

        public ApiResponseDto<bool> UpdateGolfClub(int golfClubId, UpdateGolfClubDto golfClubDto)
        {
            if (golfClubId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Golf Club Id");
            }

            var golfClub = repository.GetById(golfClubId);

            if (golfClub == null)
            {
                return new ApiResponseDto<bool>("Golf Club Not Found");
            }

            if (golfClubDto.serialNumber <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Serial Number");
            };

            if (golfClubDto.bookingId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Booking Id");
            }

            var booking = new Booking();

            if (golfClubDto.bookingId != null)
            {
                booking = bookingRepository.GetById(golfClubDto.bookingId.GetValueOrDefault());

                if (booking == null ||
                    booking.Id == 0)
                {
                    return new ApiResponseDto<bool>("Booking Not Found");
                }
            }

            mapper.Map(golfClubDto, golfClub);

            var result = repository.Update(golfClub);

            return new ApiResponseDto<bool>(result);
        }
    }
}
