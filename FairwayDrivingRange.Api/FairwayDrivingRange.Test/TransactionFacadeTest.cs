using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                customerId = -1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120
            };

            var expected = new ApiResponseDto<bool>("Invalid Customer Id");

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
                customerId = 0,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120
            };

            var expected = new ApiResponseDto<bool>("Invalid Customer Id");

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
                customerId = 1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 0
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Booking Price And Total Can Not Be 0");

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
                customerId = 1,
                clubPrice = 65,
                bookingPrice = 0,
                total = 120
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Booking Price And Total Can Not Be 0");

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
                customerId = 1,
                clubPrice = 65,
                bookingPrice = 80,
                total = 120
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Total Price Does Not Add Up");

            //Act
            var actual = transactionFacade.AddTransaction(transactionDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddTransaction_Fails_CustomerNotFound()
        {
            //Arrange
            var transactionDto = new AddTransactionDto
            {
                customerId = 1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120
            };

            var expected = new ApiResponseDto<bool>("Customer Not Found");

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
                customerId = 1,
                clubPrice = 0,
                bookingPrice = 120,
                total = 120
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

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
                    customerId = 1,
                },
                new TransactionDto
                {
                    id = 2,
                    customerId = 2,
                    clubPrice = 50,
                    bookingPrice = 120,
                    total = 170
                }
            };

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    CustomerId = 1,
                    ClubPrice = 0,
                    BookingPrice = 120,
                    Total = 120
                },
                new Transaction
                {
                    CustomerId = 2,
                    ClubPrice = 50,
                    BookingPrice = 120,
                    Total = 170
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
            Assert.Equal(expected.Value.ToList()[0].customerId, actual.Value.ToList()[0].customerId);

            Assert.Equal(expected.Value.ToList()[1].id, actual.Value.ToList()[1].id);
            Assert.Equal(expected.Value.ToList()[1].clubPrice, actual.Value.ToList()[1].clubPrice);
            Assert.Equal(expected.Value.ToList()[1].bookingPrice, actual.Value.ToList()[1].bookingPrice);
            Assert.Equal(expected.Value.ToList()[1].total, actual.Value.ToList()[1].total);
            Assert.Equal(expected.Value.ToList()[1].customerId, actual.Value.ToList()[1].customerId);
        }

        [Fact]
        public void GetTransaction_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = new ApiResponseDto<TransactionDto>("Invalid Id");

            //Act
            var actual = transactionFacade.GetTransactionById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetTransaction_Fails_IdZero()
        {
            //Arrange
            var expected = new ApiResponseDto<TransactionDto>("Invalid Id");

            //Act
            var actual = transactionFacade.GetTransactionById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetTransaction_Fails_TransactionNotFound()
        {
            //Arrange
            var expected = new ApiResponseDto<TransactionDto>("Transaction Not Found");

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
                CustomerId = 1,
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
            Assert.Equal(expected.Value.customerId, actual.Value.customerId);
        }

        [Fact]
        public void DeleteTransaction_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = transactionFacade.DeleteTransaction(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteTransaction_Fails_IdZero()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = transactionFacade.DeleteTransaction(0);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteTransaction_Fails_NumberNotFound()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Transaction Not Found");

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

            var expectedCustomers = 2;

            var addedTransactions = new List<Transaction>
            {
                new Transaction
                {
                    CustomerId = 1,
                    ClubPrice = 0,
                    BookingPrice = 120,
                    Total = 120
                },
                new Transaction
                {
                    CustomerId = 2,
                    ClubPrice = 50,
                    BookingPrice = 120,
                    Total = 170
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

            context.Transactions.AddRange(addedTransactions);

            context.SaveChanges();

            //Act
            var actual = transactionFacade.DeleteTransaction(1);

            var transactions = transactionFacade.GetTransactions();

            var transactionCount = transactions.Value.Count();

            var customers = customerFacade.GetCustomers();

            var customerCount = customers.Value.Count();

            //Assert
            Assert.Equal(customerCount, expectedCustomers);

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
                customerId = 1
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Transaction Id");

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
                customerId = 1
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Transaction Id");

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
                customerId = 1
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            context.CustomerInformation.Add(customer);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Transaction Not Found");

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
                customerId = 0
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            var transaction = new Transaction
            {              
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                CustomerId = 1
            };

            context.CustomerInformation.Add(customer);

            context.Transactions.Add(transaction);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Customer Id");

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
                customerId = 1
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            var transaction = new Transaction
            {
                ClubPrice = 0,
                BookingPrice = 120,
                Total = 120,
                CustomerId = 1
            };

            context.CustomerInformation.Add(customer);

            context.Transactions.Add(transaction);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Total Price Does Not Add Up");

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
                customerId = 2
            };

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    CustomerId = 1,
                    ClubPrice = 0,
                    BookingPrice = 120,
                    Total = 120
                },
                new Transaction
                {
                    CustomerId = 2,
                    ClubPrice = 50,
                    BookingPrice = 120,
                    Total = 170
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
            Assert.Equal(transaction.Value.customerId, transactionDto.customerId);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
