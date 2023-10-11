﻿using AutoMapper;
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
            if (string.IsNullOrWhiteSpace(golfClubDto.SerialNumber))
            {
                return ApiResponseDto<bool>.Error("Invalid Serial Number");
            };

            var golfClub = mapper.Map<GolfClub>(golfClubDto);

            var result = repository.Add(golfClub);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteGolfClub(int golfClubId)
        {
            if (golfClubId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Golf Club Id");
            };

            var golfClub = repository.GetById(golfClubId);

            if (golfClub == null)
            {
                return ApiResponseDto<bool>.Error("Golf Club Not Found");
            };

            var result = repository.Delete(golfClub);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<GolfClubDto> GetGolfClubById(int golfClubId)
        {
            if (golfClubId <= 0)
            {
                return ApiResponseDto<GolfClubDto>.Error("Invalid Golf Club Id");
            };

            var golfClub = repository.GetById(golfClubId);

            if (golfClub == null)
            {
                return ApiResponseDto<GolfClubDto>.Error("Golf Club Not Found");
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
                return ApiResponseDto<bool>.Error("Invalid Golf Club Id");
            }

            var golfClub = repository.GetById(golfClubId);

            if (golfClub == null)
            {
                return ApiResponseDto<bool>.Error("Golf Club Not Found");
            }

            if (string.IsNullOrWhiteSpace(golfClubDto.SerialNumber))
            {
                return ApiResponseDto<bool>.Error("Invalid Serial Number");
            };

            if (golfClubDto.BookingId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Booking Id");
            }

            var booking = new Booking();

            if (golfClubDto.BookingId != null)
            {
                booking = bookingRepository.GetById(golfClubDto.BookingId.GetValueOrDefault());

                if (booking == null ||
                    booking.Id == 0)
                {
                    return ApiResponseDto<bool>.Error("Booking Not Found");
                }
            }

            mapper.Map(golfClubDto, golfClub);

            var result = repository.Update(golfClub);

            return new ApiResponseDto<bool>(result);
        }
    }
}
