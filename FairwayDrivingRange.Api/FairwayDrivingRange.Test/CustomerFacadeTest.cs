using FairwayDrivingRange.Application;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure.Data;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Test
{
    public class CustomerFacadeTest : BaseTestSetup
    {
        [Fact]
        public void AddCustomer_Fails_InvalidNameCustomerDto()
        {
            //Arrange
            var customerDto = new AddCustomerDto
            {
                name = "",
                email = "thisisanEmail@email.com",
                isPaid = false
            };

            var expected = new ApiResponseDto<bool>("Invalid Customer Object");

            //Act
            var actual = customerFacade.AddCustomer(customerDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);

        }

        [Fact]
        public void AddCustomer_Fails_InvalidEmailCustomerDto()
        {
            //Arrange
            var customerDto = new AddCustomerDto
            {
                name = "Jane Doe",
                email = "",
                isPaid = false
            };

            var expected = new ApiResponseDto<bool>("Invalid Customer Object");

            //Act
            var actual = customerFacade.AddCustomer(customerDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddCustomer_Succeeds_ValidCustomerDto()
        {
            //Arrange               
            var customerDto = new AddCustomerDto
            {
                name = "Test Name",
                email = "thisisanEmail@email.com",
                isPaid = false
            };

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = customerFacade.AddCustomer(customerDto);

            //Assert
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public void GetCustomers_Succeeds()
        {
            var customerDtos = new List<CustomerDto>
                {
                    new CustomerDto
                    {
                        id = 1,
                        name = "J",
                        email = "e@gmail.com",
                        isPaid = false
                    },
                    new CustomerDto
                    {
                        id = 2,
                        name = "k",
                        email = "e@gmail.com",
                        isPaid = true
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

            context.SaveChanges();

            var expected = new ApiResponseDto<IEnumerable<CustomerDto>>(customerDtos);

            //Act
            var actual = customerFacade.GetCustomers();

            //Assert
            Assert.Equal(expected.Value.ToList()[0].id, actual.Value.ToList()[0].id);
            Assert.Equal(expected.Value.ToList()[0].name, actual.Value.ToList()[0].name);
            Assert.Equal(expected.Value.ToList()[0].email, actual.Value.ToList()[0].email);
            Assert.Equal(expected.Value.ToList()[0].isPaid, actual.Value.ToList()[0].isPaid);

            Assert.Equal(expected.Value.ToList()[1].id, actual.Value.ToList()[1].id);
            Assert.Equal(expected.Value.ToList()[1].name, actual.Value.ToList()[1].name);
            Assert.Equal(expected.Value.ToList()[1].email, actual.Value.ToList()[1].email);
            Assert.Equal(expected.Value.ToList()[1].isPaid, actual.Value.ToList()[1].isPaid);
        }

        [Fact]
        public void GetCustomer_Succeeds()
        {
            //Arrange
            var customerDto = new CustomerDto
            {
                id = 1,
                name = "J",
                email = "e@gmail.com",
                isPaid = false
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

            context.SaveChanges();

            var expected = new ApiResponseDto<CustomerDto>(customerDto);

            //Act
            var actual = customerFacade.GetCustomerById(1);

            //Assert
            Assert.Equal(expected.Value.id, actual.Value.id);
            Assert.Equal(expected.Value.name, actual.Value.name);
            Assert.Equal(expected.Value.email, actual.Value.email);
            Assert.Equal(expected.Value.isPaid, actual.Value.isPaid);
        }

        [Fact]
        public void GetCustomer_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = customerFacade.GetCustomerById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetCustomer_Fails_IdZero()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = customerFacade.GetCustomerById(0);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
            
        }

        [Fact]
        public void GetCustomer_Fails_NumberNotFound()
        {
            
            //Arrange
            var expected = new ApiResponseDto<bool>("Customer Not Found");

            //Act
            var actual = customerFacade.GetCustomerById(30);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);

        }

        [Fact]
        public void DeleteCustomer_Fails_IdNegativeNumber() 
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = customerFacade.DeleteCustomer(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }
        
        [Fact]
        public void DeleteCustomer_Fails_IdZero() 
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = customerFacade.DeleteCustomer(0);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteCustomer_Fails_NumberNotFound() 
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Customer Not Found");

            //Act
            var actual = customerFacade.DeleteCustomer(3);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteCustomer_Succeeds_CustomerDeleted()
        {
            //Arrange
            var expected = 1;

            var addCustomers = new List<CustomerInformation>
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

            context.CustomerInformation.AddRange(addCustomers);

            context.SaveChanges();

            //Act
            var actual = customerFacade.DeleteCustomer(1);

            var customers = customerFacade.GetCustomers();

            var customerCount = customers.Value.Count();

            //Assert
            Assert.Equal(customerCount, expected);
        }

        [Fact]
        public void UpdateCustomer_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = customerFacade.UpdateCustomer(-1, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }
        
        [Fact]
        public void UpdateCustomer_Fails_IdZero()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Invalid Id");

            //Act
            var actual = customerFacade.UpdateCustomer(0, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateCustomer_Fails_NumberNotFound()
        {
            //Arrange
            var expected = new ApiResponseDto<bool>("Customer Not Found");

            //Act
            var actual = customerFacade.UpdateCustomer(3, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateCustomer_Fails_InvalidDtoName()
        {
            //Arrange
            var customerDto = new AddCustomerDto
            {
                name = "",
                email = "eg@gmail.com",
                isPaid = false
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

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Customer Object");

            //Act
            var actual = customerFacade.UpdateCustomer(1, customerDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }
        
        [Fact]
        public void UpdateCustomer_Fails_InvalidDtoEmail()
        {
            //Arrange
            var customerDto = new AddCustomerDto
            {
                name = "W",
                email = "",
                isPaid = true
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

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>("Invalid Customer Object");

            //Act
            var actual = customerFacade.UpdateCustomer(1, customerDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateCustomer_Succeeds_CustomerUpdated()
        {
            //Arrange
            var customerDto = new AddCustomerDto
            {
                name = "W",
                email = "eg@gmail.com",
                isPaid = true
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

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = customerFacade.UpdateCustomer(1, customerDto);

            var customer = customerFacade.GetCustomerById(1);

            //Assert
            Assert.Equal(customer.Value.name, customerDto.name);
            Assert.Equal(customer.Value.email, customerDto.email);
            Assert.Equal(customer.Value.isPaid, customerDto.isPaid);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}