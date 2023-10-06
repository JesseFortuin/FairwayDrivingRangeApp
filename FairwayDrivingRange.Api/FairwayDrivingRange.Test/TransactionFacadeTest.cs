using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Test
{
    public class TransactionFacadeTest : BaseTestSetup
    {
        [Fact]
        public void AddTransaction_Fails_InvalidTransactionDtoIdNegativeNumber()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                bookingId = -1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Booking Id");

            //Act
            var actual = transactionFacade.AddTransaction(transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddTransaction_Fails_InvalidTransactionDtoIdZero()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                bookingId = 0,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Booking Id");

            //Act
            var actual = transactionFacade.AddTransaction(transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddTransaction_Fails_InvalidTransactionDtoTotalZero()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                bookingId = 1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 0
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(1),
            };

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Booking Price And Total Can Not Be 0");

            //Act
            var actual = transactionFacade.AddTransaction(transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddTransaction_Fails_InvalidTransactionDtoBookingPriceZero()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                bookingId = 1,
                clubPrice = 65,
                bookingPrice = 0,
                total = 120
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now, 
                End = DateTime.Now.AddHours(1),
                IsPaid = false
            };

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Booking Price And Total Can Not Be 0");

            //Act
            var actual = transactionFacade.AddTransaction(transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddTransaction_Fails_InvalidTransactionDtoTotalIsIncorrect()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                bookingId = 1,
                clubPrice = 65,
                bookingPrice = 80,
                total = 120
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                IsPaid = false
            };

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Total Price Does Not Add Up");

            //Act
            var actual = transactionFacade.AddTransaction(transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddTransaction_Fails_BookingNotFound()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                bookingId = 1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120
            };

            var expected = ApiResponseDto<bool>.Error("Booking Not Found");

            //Act
            var actual = transactionFacade.AddTransaction(transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddTransaction_Succeeds_TransactionAdded()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                bookingId = 1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                IsPaid = false
            };

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = transactionFacade.AddTransaction(transactionDto);

            //Assert
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public void GetTransactions_Succeeds()
        {
            var transactionDtos = new List<TransactionDto>
            {
                new TransactionDto
                {
                    id = 1,
                    clubPrice = 0,
                    bookingPrice = 120,
                    total = 120,
                    bookingId = 1,
                },
                new TransactionDto
                {
                    id = 2,
                    bookingId = 2,
                    clubPrice = 50,
                    bookingPrice = 120,
                    total = 170
                }
            };

            var transactions = new List<Transaction>
            {
                new Transaction
                { 
                    BookingId = 1,
                    ClubPrice = 0,
                    BookingPrice = 120,
                    Total = 120
                },
                new Transaction
                {
                    BookingId = 2,
                    ClubPrice = 50,
                    BookingPrice = 120,
                    Total = 170
                }
            };

            var bookings = new List<Booking>
            {
                new Booking
                {
                    DateBooked = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    IsPaid = false
                },
                new Booking
                {
                    DateBooked = DateTime.Now,
                    End = DateTime.Now.AddHours(2),
                    IsPaid = false
                }
            };

            context.Bookings.AddRange(bookings);

            context.Transactions.AddRange(transactions);

            context.SaveChanges();

            var expected = new ApiResponseDto<IEnumerable<TransactionDto>>(transactionDtos);

            //Act
            var actual = transactionFacade.GetTransactions();

            //Assert
            Assert.Equal(expected.Value.ToList()[0].id, actual.Value.ToList()[0].id);
            Assert.Equal(expected.Value.ToList()[0].clubPrice, actual.Value.ToList()[0].clubPrice);
            Assert.Equal(expected.Value.ToList()[0].bookingPrice, actual.Value.ToList()[0].bookingPrice);
            Assert.Equal(expected.Value.ToList()[0].total, actual.Value.ToList()[0].total);
            Assert.Equal(expected.Value.ToList()[0].bookingId, actual.Value.ToList()[0].bookingId);

            Assert.Equal(expected.Value.ToList()[1].id, actual.Value.ToList()[1].id);
            Assert.Equal(expected.Value.ToList()[1].clubPrice, actual.Value.ToList()[1].clubPrice);
            Assert.Equal(expected.Value.ToList()[1].bookingPrice, actual.Value.ToList()[1].bookingPrice);
            Assert.Equal(expected.Value.ToList()[1].total, actual.Value.ToList()[1].total);
            Assert.Equal(expected.Value.ToList()[1].bookingId, actual.Value.ToList()[1].bookingId);
        }

        [Fact]
        public void GetTransaction_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = ApiResponseDto<TransactionDto>.Error("Invalid Id");

            //Act
            var actual = transactionFacade.GetTransactionById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetTransaction_Fails_IdZero()
        {
            //Arrange
            var expected = ApiResponseDto<TransactionDto>.Error("Invalid Id");

            //Act
            var actual = transactionFacade.GetTransactionById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetTransaction_Fails_TransactionNotFound()
        {
            //Arrange
            var expected = ApiResponseDto<TransactionDto>.Error("Transaction Not Found");

            //Act
            var actual = transactionFacade.GetTransactionById(1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetTransaction_Succeeds_TransactionFound()
        {
            //Arrange
            var transaction = new Transaction
            {
                BookingId = 1,
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120
            };

            var transactionDto = new TransactionDto
            {
                id = 1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120,
                bookingId = 1
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                IsPaid = false
            };

            var id = 1;

            context.Bookings.Add(booking);

            context.Transactions.Add(transaction);

            context.SaveChanges();

            var expected = new ApiResponseDto<TransactionDto>(transactionDto);

            //Act
            var actual = transactionFacade.GetTransactionById(id);

            //Assert
            Assert.Equal(expected.Value.id, actual.Value.id);
            Assert.Equal(expected.Value.clubPrice, actual.Value.clubPrice);
            Assert.Equal(expected.Value.bookingPrice, actual.Value.bookingPrice);
            Assert.Equal(expected.Value.total, actual.Value.total);
            Assert.Equal(expected.Value.bookingId, actual.Value.bookingId);
        }

        [Fact]
        public void DeleteTransaction_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = transactionFacade.DeleteTransaction(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteTransaction_Fails_IdZero()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = transactionFacade.DeleteTransaction(0);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteTransaction_Fails_NumberNotFound()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Transaction Not Found");

            //Act
            var actual = transactionFacade.DeleteTransaction(3);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteTransaction_Succeeds_TransactionDeleted()
        {
            //Arrange
            var expectedTransactions = 1;

            var expectedBookings = 2;

            var addedTransactions = new List<Transaction>
            {
                new Transaction
                {
                    BookingId = 1,
                    ClubPrice = 0,
                    BookingPrice = 120,
                    Total = 120
                },
                new Transaction
                {
                    BookingId = 2,
                    ClubPrice = 50,
                    BookingPrice = 120,
                    Total = 170
                }
            };

            var addedBookings = new List<Booking>
            {
                new Booking
                {
                    DateBooked = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    IsPaid = false
                },
                new Booking
                {
                    DateBooked = DateTime.Now,
                    End = DateTime.Now.AddHours(2),
                    IsPaid = false
                }
            };

            context.Bookings.AddRange(addedBookings);

            context.Transactions.AddRange(addedTransactions);

            context.SaveChanges();

            //Act
            var actual = transactionFacade.DeleteTransaction(1);

            var transactions = transactionFacade.GetTransactions();

            var transactionCount = transactions.Value.Count();

            var bookings = bookingFacade.GetBookings();

            var bookingCount = bookings.Value.Count();

            //Assert
            Assert.Equal(bookingCount, expectedBookings);

            Assert.Equal(transactionCount, expectedTransactions);
        }

        [Fact]
        public void UpdateTransaction_Fails_IdNegativeNumber()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                clubPrice = 0,
                bookingPrice = 120,
                total = 120,
                bookingId = 1
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                IsPaid = false
            };

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Invalid Transaction Id");

            //Act
            var actual = transactionFacade.UpdateTransaction(-1, transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateTransaction_Fails_IdZero()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                clubPrice = 0,
                bookingPrice = 120,
                total = 120,
                bookingId = 1
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                IsPaid = false
            };

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Invalid Transaction Id");

            //Act
            var actual = transactionFacade.UpdateTransaction(0, transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateTransaction_Fails_NumberNotFound()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                clubPrice = 0,
                bookingPrice = 120,
                total = 120,
                bookingId = 1
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                IsPaid = false
            };

            context.Bookings.Add(booking);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Transaction Not Found");

            //Act
            var actual = transactionFacade.UpdateTransaction(3, transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateTransaction_Fails_InvalidDtoCustomerId()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                clubPrice = 0,
                bookingPrice = 120,
                total = 120,
                bookingId = 0
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                IsPaid = false
            };

            var transaction = new Transaction
            {
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                BookingId = 1
            };

            context.Bookings.Add(booking);

            context.Transactions.Add(transaction);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Invalid Booking Id");

            //Act
            var actual = transactionFacade.UpdateTransaction(1, transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateTransaction_Fails_InvalidDtoTotalIncorrect()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                clubPrice = 5,
                bookingPrice = 120,
                total = 120,
                bookingId = 1
            };

            var booking = new Booking
            {
                DateBooked = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                IsPaid = false
            };

            var transaction = new Transaction
            {
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                BookingId = 1
            };

            context.Bookings.Add(booking);

            context.Transactions.Add(transaction);

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Total Price Does Not Add Up");

            //Act
            var actual = transactionFacade.UpdateTransaction(1, transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateCustomer_Succeeds_CustomerUpdated()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                clubPrice = 5,
                bookingPrice = 120,
                total = 125,
                bookingId = 2
            };

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    BookingId = 1,
                    ClubPrice = 0,
                    BookingPrice = 120,
                    Total = 120
                },
                new Transaction
                {
                    BookingId = 2,
                    ClubPrice = 50,
                    BookingPrice = 120,
                    Total = 170
                }
            };

            var bookings = new List<Booking>
            {
                new Booking
                {
                    DateBooked = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    IsPaid = false
                },
                new Booking
                {
                    DateBooked = DateTime.Now,
                    End = DateTime.Now.AddHours(2),
                    IsPaid = false
                }
            };

            context.Bookings.AddRange(bookings);

            context.Transactions.AddRange(transactions);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = transactionFacade.UpdateTransaction(1, transactionDto);

            var transaction = transactionFacade.GetTransactionById(1);

            //Assert
            Assert.Equal(transaction.Value.clubPrice, transactionDto.clubPrice);
            Assert.Equal(transaction.Value.bookingPrice, transactionDto.bookingPrice);
            Assert.Equal(transaction.Value.total, transactionDto.total);
            Assert.Equal(transaction.Value.bookingId, transactionDto.bookingId);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
