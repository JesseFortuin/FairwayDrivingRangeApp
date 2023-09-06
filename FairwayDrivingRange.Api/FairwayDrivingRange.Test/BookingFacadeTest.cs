using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Test
{
    public class BookingFacadeTest : BaseTestSetup
    {
        [Fact]
        public void AddBooking_Fails_InvalidBookingDtoIdNegativeNumber()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = -1,
                dateBooked = new DateTime(),
                lane = 4,
            };

            var expected = new ApiResponseDto<bool>("Invalid Customer Id");

            //Act
            var actual = bookingFacade.AddBooking(bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }
        
        [Fact]
        public void AddBooking_Fails_InvalidBookingDtoIdZero()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 0,
                dateBooked = DateTime.Now,
                lane = 4,
            };

            var expected = new ApiResponseDto<bool>("Invalid Customer Id");

            //Act
            var actual = bookingFacade.AddBooking(bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddBooking_Fails_CustomerNotFound()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 1,
                dateBooked = DateTime.Now,
                lane = 4,
            };

            var expected = new ApiResponseDto<bool>("Customer Not Found");

            //Act
            var actual = bookingFacade.AddBooking(bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddBooking_Succeeds_BookingAdded()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 1,
                dateBooked = DateTime.Now,
                lane = 4,
            };

            var booking = new Booking
            {
                CustomerId = 1,
                DateBooked = DateTime.Now,
                Lane = 4,
            };

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = bookingFacade.AddBooking(bookingDto);

            //Assert
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
