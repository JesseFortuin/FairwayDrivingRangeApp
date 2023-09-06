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
    public class BookingFacade : IBookingFacade
    {
        private readonly IRepository<Booking> repository;

        public BookingFacade(IRepository<Booking> repository)
        {
            this.repository = repository;
        }

        public ApiResponseDto<bool> AddBooking(AddBookingDto bookingDto)
        {
            if (bookingDto.customerId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Customer Id");
            }

            var customer = repository.GetById(bookingDto.customerId);

            if (customer == null) 
            {
                return new ApiResponseDto<bool>("Customer Not Found");
            }

            var booking = new Booking
            {
                DateBooked = bookingDto.dateBooked,
                
                Lane = bookingDto.lane,

                CustomerId = customer.Id
            };

            var result = repository.Create(booking);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<BookingDto> GetBookingById(int bookingId)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<IEnumerable<BookingDto>> GetBookings()
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<bool> UpdateBooking(int bookingId, AddBookingDto bookingDto)
        {
            throw new NotImplementedException();
        }
    }
}
