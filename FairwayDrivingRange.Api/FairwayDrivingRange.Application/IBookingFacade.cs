﻿using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Application
{
    public interface IBookingFacade
    {
        public ApiResponseDto<bool> AddBooking(AddBookingDto bookingDto);

        public ApiResponseDto<bool> UpdateBooking(int bookingId, AddBookingDto bookingDto);

        public ApiResponseDto<IEnumerable<BookingDto>> GetBookings();

        public ApiResponseDto<BookingDto> GetBookingById(int bookingId);

        public ApiResponseDto<bool> DeleteBooking(int bookingId);
    }
}
