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
                BookingId = -1,
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120
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
                BookingId = 0,
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120
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
                BookingId = 1,
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 0
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
                BookingId = 1,
                ClubPrice = 65,
                BookingPrice = 0,
                Total = 120
            };

            var booking = new Booking
            {
                Start = DateTime.Now, 
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
                BookingId = 1,
                ClubPrice = 65,
                BookingPrice = 80,
                Total = 120
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
                BookingId = 1,
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120
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
                BookingId = 1,
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
                    Id = 1,
                    ClubPrice = 0,
                    BookingPrice = 120,
                    Total = 120,
                    BookingId = 1,
                },
                new TransactionDto
                {
                    Id = 2,
                    BookingId = 2,
                    ClubPrice = 50,
                    BookingPrice = 120,
                    Total = 170
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
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    IsPaid = false
                },
                new Booking
                {
                    Start = DateTime.Now,
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
            Assert.Equal(expected.Value.ToList()[0].Id, actual.Value.ToList()[0].Id);
            Assert.Equal(expected.Value.ToList()[0].ClubPrice, actual.Value.ToList()[0].ClubPrice);
            Assert.Equal(expected.Value.ToList()[0].BookingPrice, actual.Value.ToList()[0].BookingPrice);
            Assert.Equal(expected.Value.ToList()[0].Total, actual.Value.ToList()[0].Total);
            Assert.Equal(expected.Value.ToList()[0].BookingId, actual.Value.ToList()[0].BookingId);

            Assert.Equal(expected.Value.ToList()[1].Id, actual.Value.ToList()[1].Id);
            Assert.Equal(expected.Value.ToList()[1].ClubPrice, actual.Value.ToList()[1].ClubPrice);
            Assert.Equal(expected.Value.ToList()[1].BookingPrice, actual.Value.ToList()[1].BookingPrice);
            Assert.Equal(expected.Value.ToList()[1].Total, actual.Value.ToList()[1].Total);
            Assert.Equal(expected.Value.ToList()[1].BookingId, actual.Value.ToList()[1].BookingId);
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
                Id = 1,
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                BookingId = 1
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
            Assert.Equal(expected.Value.Id, actual.Value.Id);
            Assert.Equal(expected.Value.ClubPrice, actual.Value.ClubPrice);
            Assert.Equal(expected.Value.BookingPrice, actual.Value.BookingPrice);
            Assert.Equal(expected.Value.Total, actual.Value.Total);
            Assert.Equal(expected.Value.BookingId, actual.Value.BookingId);
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
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    IsPaid = false
                },
                new Booking
                {
                    Start = DateTime.Now,
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
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                BookingId = 1
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                BookingId = 1
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                BookingId = 1
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                BookingId = 0
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
                ClubPrice = 5,
                BookingPrice = 120,
                Total = 120,
                BookingId = 1
            };

            var booking = new Booking
            {
                Start = DateTime.Now,
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
                ClubPrice = 5,
                BookingPrice = 120,
                Total = 125,
                BookingId = 2
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
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    IsPaid = false
                },
                new Booking
                {
                    Start = DateTime.Now,
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
            Assert.Equal(transaction.Value.ClubPrice, transactionDto.ClubPrice);
            Assert.Equal(transaction.Value.BookingPrice, transactionDto.BookingPrice);
            Assert.Equal(transaction.Value.Total, transactionDto.Total);
            Assert.Equal(transaction.Value.BookingId, transactionDto.BookingId);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
