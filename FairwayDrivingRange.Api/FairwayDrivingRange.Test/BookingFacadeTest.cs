using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Test
{
    public class BookingFacadeTest : BaseTestSetup
    {
        [Fact]
        public void AddBookingEmail_Fails_InvalidBoookingDtoEmailEmpty()
        {
            //Arrange
            var bookingDto = new AddBookingEmailDto
            {
                Email = "",
                Start = new DateTime(),
                End = DateTime.Now.AddHours(1),
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Email");

            //Act
            var actual = bookingFacade.AddBookingEmail(bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddBookingEmail_Fails_InvalidBoookingDtoEmailnull()
        {
            //Arrange
            var bookingDto = new AddBookingEmailDto
            {
                Email = null,
                Start = new DateTime(),
                End = DateTime.Now.AddHours(1),
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Email");
            
            //Act
            var actual = bookingFacade.AddBookingEmail(bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddBookingEmail_Fails_CustomerNotFound()
        {
            //Arrange
            var bookingDto = new AddBookingEmailDto
            {
                Email = "bingbong@gmail.com",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };

            var expected = ApiResponseDto<bool>.Error("Customer Not Found");

            //Act
            var actual = bookingFacade.AddBookingEmail(bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddBookingEmail_Succeeds_BookingAdded()
        {
            //Arrange
            var bookingDto = new AddBookingEmailDto
            {
                Email = "e@gmail.com",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };

            var booking = new Booking
            {
                CustomerId = 1,
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com"
            };

            context.CustomerInformation.Add(customer);

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = bookingFacade.AddBookingEmail(bookingDto);

            //Assert
            Assert.Equal(expected.Value, actual.Value);
        }

        //[Fact]
        //public void AddBooking_Fails_InvalidDtoLaneOverTwenty()
        //{
        //    //Arrange
        //    var bookingDto = new AddBookingDto
        //    {
        //        customerId = 1,
        //        start = DateTime.Now,
        //        end = DateTime.Now.AddHours(1)
        //    };

        //    var customer = new CustomerInformation
        //    {
        //        Name = "J",
        //        Email = "e@gmail.com",
        //        IsPaid = false
        //    };

        //    context.CustomerInformation.Add(customer);

        //    context.SaveChanges();

        //    var expected = ApiResponseDto<bool>.Error("Invalid Booking Object");

        //    //Act
        //    var actual = bookingFacade.AddBooking(bookingDto);

        //    //Assert
        //    Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        //}

        //[Fact]
        //public void AddBooking_Fails_InvalidDtoLaneZero()
        //{
        //    //Arrange
        //    var bookingDto = new AddBookingDto
        //    {
        //        customerId = 1,
        //        start = DateTime.Now,
        //        end = DateTime.Now.AddHours(1)
        //    };

        //    var customer = new CustomerInformation
        //    {
        //        Name = "J",
        //        Email = "e@gmail.com",
        //        IsPaid = false
        //    };

        //    context.CustomerInformation.Add(customer);

        //    context.SaveChanges();

        //    var expected = ApiResponseDto<bool>.Error("Invalid Booking Object");

        //    //Act
        //    var actual = bookingFacade.AddBooking(bookingDto);

        //    //Assert
        //    Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        //}

        //[Fact]
        //public void AddBooking_Fails_InvalidDtoLaneNegativeNumber()
        //{
        //    //Arrange
        //    var bookingDto = new AddBookingDto
        //    {
        //        customerId = 1,
        //        start = DateTime.Now,
        //        end = DateTime.Now.AddHours(1)
        //    };

        //    var customer = new CustomerInformation
        //    {
        //        Name = "J",
        //        Email = "e@gmail.com",
        //        IsPaid = false
        //    };

        //    context.CustomerInformation.Add(customer);

        //    context.SaveChanges();

        //    var expected = ApiResponseDto<bool>.Error("Invalid Booking Object");

        //    //Act
        //    var actual = bookingFacade.AddBooking(bookingDto);

        //    //Assert
        //    Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        //}

        [Fact]
        public void AddBooking_Succeeds_BookingAdded()
        {
            //Arrange
            var bookingDto = new AddBookingDto
            {
                Start = DateTime.Now,
                End= DateTime.Now.AddHours(1),
                Name = "J",
                Email = "e@gmail.com"
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com"
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
                    Id = 1,
                    Start = DateTime.Today,
                    End = DateTime.Today
                },
                new BookingDto
                {
                    Id = 2,
                    Start = DateTime.Today,
                    End = DateTime.Today
                }
            };

            var bookings = new List<Booking>
            {
                new Booking
                {
                    Start = DateTime.Today,
                    End = DateTime.Today,
                    CustomerId = 1
                },
                new Booking
                {
                    Start = DateTime.Today,
                    End = DateTime.Today,
                    CustomerId = 2
                }
            };

            var customers = new List<CustomerInformation>
            {
                new CustomerInformation
                {
                    Name = "J",
                    Email = "e@gmail.com"
                },
                new CustomerInformation
                {
                    Name = "k",
                    Email = "e@gmail.com"
                }
            };

            context.CustomerInformation.AddRange(customers);

            context.Bookings.AddRange(bookings);

            context.SaveChanges();

            var expected = new ApiResponseDto<IEnumerable<BookingDto>>(bookingDtos);

            //Act
            var actual = bookingFacade.GetBookings();

            //Assert
            Assert.Equal(expected.Value.ToList()[0].Id, actual.Value.ToList()[0].Id);
            Assert.Equal(expected.Value.ToList()[0].End, actual.Value.ToList()[0].End);
            Assert.Equal(expected.Value.ToList()[0].Start, actual.Value.ToList()[0].Start);

            Assert.Equal(expected.Value.ToList()[1].Id, actual.Value.ToList()[1].Id);
            Assert.Equal(expected.Value.ToList()[1].Start, actual.Value.ToList()[1].Start);
            Assert.Equal(expected.Value.ToList()[1].End, actual.Value.ToList()[1].End);
        }

        [Fact]
        public void GetBooking_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = ApiResponseDto<BookingDto>.Error("Invalid Id");

            //Act
            var actual = bookingFacade.GetBookingById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetBooking_Fails_IdZero()
        {
            //Arrange
            var expected = ApiResponseDto<BookingDto>.Error("Invalid Id");

            //Act
            var actual = bookingFacade.GetBookingById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetBooking_Fails_BookingNotFound()
        {
            //Arrange
            var expected = ApiResponseDto<BookingDto>.Error("Booking Not Found");

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
                Start = DateTime.Today,
                End = DateTime.Today,
                CustomerId = 1
            };

            var bookingDto = new BookingDto
            {
                Id = 1,
                Start = DateTime.Today,
                End = DateTime.Today
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com"
            };

            var id = 1;

            context.CustomerInformation.Add(customer);

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = new ApiResponseDto<BookingDto>(bookingDto);

            //Act
            var actual = bookingFacade.GetBookingById(id);

            //Assert
            Assert.Equal(expected.Value.Id, actual.Value.Id);
            Assert.Equal(expected.Value.End, actual.Value.End);
            Assert.Equal(expected.Value.Start, actual.Value.Start);
        }

        [Fact]
        public void DeleteBooking_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = bookingFacade.DeleteBooking(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteBooking_Fails_IdZero()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = bookingFacade.DeleteBooking(0);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteBooking_Fails_NumberNotFound()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Booking Not Found");

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
                    Start = DateTime.Today,
                    End = DateTime.Today,
                    CustomerId = 1
                },
                new Booking
                {
                    Start = DateTime.Today,
                    End = DateTime.Today,
                    CustomerId = 2
                }
            };

            var addedCustomers = new List<CustomerInformation>
            {
                new CustomerInformation
                {
                    Name = "J",
                    Email = "e@gmail.com"
                },
                new CustomerInformation
                {
                    Name = "k",
                    Email = "e@gmail.com"
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
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = bookingFacade.UpdateBooking(-1, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateBooking_Fails_IdZero()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = bookingFacade.UpdateBooking(0, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateBooking_Fails_NumberNotFound()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Booking Not Found");

            //Act
            var actual = bookingFacade.UpdateBooking(3, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateBooking_Fails_InvalidDtoCustomerId()
        {
            //Arrange
            var bookingDto = new UpdateBookingDto
            {
                CustomerId = 0,
                Start = DateTime.Now,
                End = DateTime.Today,
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com"
            };

            var booking = new Booking
            {
                Start = DateTime.Today,
                End = DateTime.Today,
                CustomerId = 1
            };

            context.CustomerInformation.Add(customer);

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Invalid Booking Object");

            //Act
            var actual = bookingFacade.UpdateBooking(1, bookingDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        //[Fact]
        //public void UpdateBooking_Fails_InvalidDtoLane()
        //{
        //    Arrange
        //    var bookingDto = new UpdateBookingDto
        //    {
        //        customerId = 1,
        //        start = DateTime.Now,
        //        lane = 0,
        //    };

        //    var customer = new CustomerInformation
        //    {
        //        Name = "J",
        //        Email = "e@gmail.com",
        //        IsPaid = false
        //    };

        //    var booking = new Booking
        //    {
        //        DateBooked = DateTime.Today,
        //        End = DateTime.Today,
        //        CustomerId = 1
        //    };

        //    context.CustomerInformation.Add(customer);

        //    context.Bookings.Add(booking);

        //    context.SaveChanges();

        //    var expected = ApiResponseDto<bool>.Error("Invalid Booking Object");

        //    Act
        //    var actual = bookingFacade.UpdateBooking(1, bookingDto);

        //    Assert
        //    Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        //}

        [Fact]
        public void UpdateCustomer_Succeeds_CustomerUpdated()
        {
            //Arrange
            var bookingDto = new UpdateBookingDto
            {
                Start = DateTime.Now,
                End = DateTime.Today,
                CustomerId = 2
            };

            var bookings = new List<Booking>
            {
                new Booking
                {
                    Start = DateTime.Today,
                    End = DateTime.Today,
                    CustomerId = 1
                },
                new Booking
                {
                    Start = DateTime.Today,
                    End = DateTime.Today,
                    CustomerId = 2
                }
            };

            var customers = new List<CustomerInformation>
            {
                new CustomerInformation
                {
                    Name = "J",
                    Email = "e@gmail.com"
                },
                new CustomerInformation
                {
                    Name = "k",
                    Email = "e@gmail.com"
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
            Assert.Equal(booking.Value.Start, bookingDto.Start);
            Assert.Equal(booking.Value.End, bookingDto.End);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
