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
        public void AddBooking_Fails_InvalidDtoLaneOverTwenty()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 1,
                dateBooked = DateTime.Now,
                lane = 21,
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Booking Object");

            //Act
            var actual = bookingFacade.AddBooking(bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddBooking_Fails_InvalidDtoLaneZero()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 1,
                dateBooked = DateTime.Now,
                lane = 0,
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Booking Object");

            //Act
            var actual = bookingFacade.AddBooking(bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddBooking_Fails_InvalidDtoLaneNegativeNumber()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 1,
                dateBooked = DateTime.Now,
                lane = -1,
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Booking Object");

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

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = bookingFacade.AddBooking(bookingDto);

            //Assert
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public void GetBookings_Succeeds()
        {
            var bookingDtos = new List<BookingDto>
            {
                new BookingDto
                {
                    id = 1,
                    dateBooked = DateTime.Today,
                    lane = 1,
                    customerId = 1
                },
                new BookingDto
                {
                    id = 2,
                    dateBooked = DateTime.Today,
                    lane = 2,
                    customerId = 2
                }
            };

            var bookings = new List<Booking>
            {
                new Booking
                {
                    DateBooked = DateTime.Today,
                    Lane = 1,
                    CustomerId = 1
                },
                new Booking
                {
                    DateBooked = DateTime.Today,
                    Lane = 2,
                    CustomerId = 2
                }
            };

            var customers = new List<CustomerInformation>
            {
                new CustomerInformation
                {
                    Name = "J",
                    Email = "e@gmail.com",
                    IsPaid = false
                },
                new CustomerInformation
                {
                    Name = "k",
                    Email = "e@gmail.com",
                    IsPaid = true
                }
            };

            context.CustomerInformation.AddRange(customers);

            context.Bookings.AddRange(bookings);

            context.SaveChanges();

            var expected = new ApiResponseDto<IEnumerable<BookingDto>>(bookingDtos);

            //Act
            var actual = bookingFacade.GetBookings();

            //Assert
            Assert.Equal(expected.Value.ToList()[0].id, actual.Value.ToList()[0].id);
            Assert.Equal(expected.Value.ToList()[0].dateBooked, actual.Value.ToList()[0].dateBooked);
            Assert.Equal(expected.Value.ToList()[0].lane, actual.Value.ToList()[0].lane);
            Assert.Equal(expected.Value.ToList()[0].customerId, actual.Value.ToList()[0].customerId);

            Assert.Equal(expected.Value.ToList()[1].id, actual.Value.ToList()[1].id);
            Assert.Equal(expected.Value.ToList()[1].dateBooked, actual.Value.ToList()[1].dateBooked);
            Assert.Equal(expected.Value.ToList()[1].lane, actual.Value.ToList()[1].lane);
            Assert.Equal(expected.Value.ToList()[1].customerId, actual.Value.ToList()[1].customerId);
        }

        [Fact]
        public void GetBooking_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = new ApiResponseDto<BookingDto>("Invalid Id");

            //Act
            var actual = bookingFacade.GetBookingById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]             
        public void GetBooking_Fails_IdZero()
        {
            //Arrange
            var expected = new ApiResponseDto<BookingDto>("Invalid Id");

            //Act
            var actual = bookingFacade.GetBookingById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetBooking_Fails_BookingNotFound()
        {
            //Arrange
            var expected = new ApiResponseDto<BookingDto>("Booking Not Found");

            //Act
            var actual = bookingFacade.GetBookingById(1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetBooking_Succeeds_BookingFound()
        {
            //Arrange
            var booking = new Booking 
            {
                DateBooked = DateTime.Today,
                Lane = 1,
                CustomerId = 1
            };

            var bookingDto = new BookingDto
            {
                id = 1,
                dateBooked = DateTime.Today,
                lane = 1,
                customerId = 1
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            var id = 1;

            context.CustomerInformation.Add(customer);

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = new ApiResponseDto<BookingDto>(bookingDto);

            //Act
            var actual = bookingFacade.GetBookingById(id);

            //Assert
            Assert.Equal(expected.Value.id, actual.Value.id);
            Assert.Equal(expected.Value.lane, actual.Value.lane);
            Assert.Equal(expected.Value.dateBooked, actual.Value.dateBooked);
            Assert.Equal(expected.Value.customerId, actual.Value.customerId);
        }

        [Fact]
        public void DeleteBooking_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = bookingFacade.DeleteBooking(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteBooking_Fails_IdZero()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = bookingFacade.DeleteBooking(0);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteBooking_Fails_NumberNotFound()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Booking Not Found");

            //Act
            var actual = bookingFacade.DeleteBooking(3);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteBooking_Succeeds_BookingDeleted()
        {
            //Arrange
            var expectedBookings = 1;

            var expectedCustomers = 2;

            var addedbookings = new List<Booking>
            {
                new Booking
                {
                    DateBooked = DateTime.Today,
                    Lane = 1,
                    CustomerId = 1
                },
                new Booking
                {
                    DateBooked = DateTime.Today,
                    Lane = 2,
                    CustomerId = 2
                }
            };

            var addedCustomers = new List<CustomerInformation>
            {
                new CustomerInformation
                {
                    Name = "J",
                    Email = "e@gmail.com",
                    IsPaid = false
                },
                new CustomerInformation
                {
                    Name = "k",
                    Email = "e@gmail.com",
                    IsPaid = true
                }
            };

            context.CustomerInformation.AddRange(addedCustomers);

            context.Bookings.AddRange(addedbookings);

            context.SaveChanges();

            //Act
            var actual = bookingFacade.DeleteBooking(1);

            var bookings = bookingFacade.GetBookings();

            var bookingCount = bookings.Value.Count();
            
            var customers = customerFacade.GetCustomers();

            var customerCount = customers.Value.Count();

            //Assert
            Assert.Equal(customerCount, expectedCustomers);

            Assert.Equal(bookingCount, expectedBookings);
        }

        [Fact]
        public void UpdateBooking_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = bookingFacade.UpdateBooking(-1, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateBooking_Fails_IdZero()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = bookingFacade.UpdateBooking(0, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateBooking_Fails_NumberNotFound()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Booking Not Found");

            //Act
            var actual = bookingFacade.UpdateBooking(3, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateBooking_Fails_InvalidDtoCustomerId()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 0,
                dateBooked = DateTime.Now,
                lane = 4,
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Today,
                Lane = 1,
                CustomerId = 1
            };

            context.CustomerInformation.Add(customer);

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Booking Object");

            //Act
            var actual = bookingFacade.UpdateBooking(1, bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateBooking_Fails_InvalidDtoLane()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 1,
                dateBooked = DateTime.Now,
                lane = 0,
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Today,
                Lane = 1,
                CustomerId = 1
            };

            context.CustomerInformation.Add(customer);

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Booking Object");

            //Act
            var actual = bookingFacade.UpdateBooking(1, bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateCustomer_Succeeds_CustomerUpdated()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                customerId = 2,
                dateBooked = DateTime.Now,
                lane = 6,
            };

            var bookings = new List<Booking>
            {
                new Booking
                {
                    DateBooked = DateTime.Today,
                    Lane = 1,
                    CustomerId = 1
                },
                new Booking
                {
                    DateBooked = DateTime.Today,
                    Lane = 2,
                    CustomerId = 2
                }
            };

            var customers = new List<CustomerInformation>
            {
                new CustomerInformation
                {
                    Name = "J",
                    Email = "e@gmail.com",
                    IsPaid = false
                },
                new CustomerInformation
                {
                    Name = "k",
                    Email = "e@gmail.com",
                    IsPaid = true
                }
            };

            context.CustomerInformation.AddRange(customers);

            context.Bookings.AddRange(bookings);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = bookingFacade.UpdateBooking(1, bookingDto);

            var booking = bookingFacade.GetBookingById(1);

            //Assert
            Assert.Equal(booking.Value.dateBooked, bookingDto.dateBooked);
            Assert.Equal(booking.Value.lane, bookingDto.lane);
            Assert.Equal(booking.Value.customerId, bookingDto.customerId);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
