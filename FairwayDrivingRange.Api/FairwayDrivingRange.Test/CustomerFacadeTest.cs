using FairwayDrivingRange.Domain.Entities;
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
                Name = "",
                Email = "thisisanEmail@email.com"
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Customer Object");

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
                Name = "Jane Doe",
                Email = ""
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Customer Object");

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
                Name = "Test Name",
                Email = "thisisanEmail@email.com"
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
                        Id = 1,
                        Name = "J",
                        Email = "e@gmail.com"
                    },
                    new CustomerDto
                    {
                        Id = 2,
                        Name = "k",
                        Email = "e@gmail.com"
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

            context.SaveChanges();

            var expected = new ApiResponseDto<IEnumerable<CustomerDto>>(customerDtos);

            //Act
            var actual = customerFacade.GetCustomers();

            //Assert
            Assert.Equal(expected.Value.ToList()[0].Id, actual.Value.ToList()[0].Id);
            Assert.Equal(expected.Value.ToList()[0].Name, actual.Value.ToList()[0].Name);
            Assert.Equal(expected.Value.ToList()[0].Email, actual.Value.ToList()[0].Email);

            Assert.Equal(expected.Value.ToList()[1].Id, actual.Value.ToList()[1].Id);
            Assert.Equal(expected.Value.ToList()[1].Name, actual.Value.ToList()[1].Name);
            Assert.Equal(expected.Value.ToList()[1].Email, actual.Value.ToList()[1].Email);
        }

        [Fact]
        public void GetCustomer_Succeeds()
        {
            //Arrange
            var customerDto = new CustomerDto
            {
                Id = 1,
                Name = "J",
                Email = "e@gmail.com"
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

            context.SaveChanges();

            var expected = new ApiResponseDto<CustomerDto>(customerDto);

            //Act
            var actual = customerFacade.GetCustomerById(1);

            //Assert
            Assert.Equal(expected.Value.Id, actual.Value.Id);
            Assert.Equal(expected.Value.Name, actual.Value.Name);
            Assert.Equal(expected.Value.Email, actual.Value.Email);
        }

        [Fact]
        public void GetCustomer_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = customerFacade.GetCustomerById(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetCustomer_Fails_IdZero()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = customerFacade.GetCustomerById(0);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);

        }

        [Fact]
        public void GetCustomer_Fails_NumberNotFound()
        {

            //Arrange
            var expected = ApiResponseDto<bool>.Error("Customer Not Found");

            //Act
            var actual = customerFacade.GetCustomerById(30);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);

        }

        [Fact]
        public void DeleteCustomer_Fails_IdNegativeNumber()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = customerFacade.DeleteCustomer(-1);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteCustomer_Fails_IdZero()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = customerFacade.DeleteCustomer(0);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteCustomer_Fails_NumberNotFound()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Customer Not Found");

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
                    Email = "e@gmail.com"
                },
                new CustomerInformation
                {
                    Name = "k",
                    Email = "e@gmail.com"
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
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = customerFacade.UpdateCustomer(-1, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateCustomer_Fails_IdZero()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Id");

            //Act
            var actual = customerFacade.UpdateCustomer(0, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateCustomer_Fails_NumberNotFound()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Customer Not Found");

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
                Name = "",
                Email = "eg@gmail.com"
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

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Invalid Customer Object");

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
                Name = "W",
                Email = ""
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

            context.SaveChanges();

            var expected = ApiResponseDto<bool>.Error("Invalid Customer Object");

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
                Name = "W",
                Email = "eg@gmail.com"
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

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = customerFacade.UpdateCustomer(1, customerDto);

            var customer = customerFacade.GetCustomerById(1);

            //Assert
            Assert.Equal(customer.Value.Name, customerDto.Name);
            Assert.Equal(customer.Value.Email, customerDto.Email);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}