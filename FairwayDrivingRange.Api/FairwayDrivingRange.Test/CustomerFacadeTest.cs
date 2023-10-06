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
                name = "",
                email = "thisisanEmail@email.com"
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
                name = "Jane Doe",
                email = ""
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
                name = "Test Name",
                email = "thisisanEmail@email.com"
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
                        email = "e@gmail.com"
                    },
                    new CustomerDto
                    {
                        id = 2,
                        name = "k",
                        email = "e@gmail.com"
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
            Assert.Equal(expected.Value.ToList()[0].id, actual.Value.ToList()[0].id);
            Assert.Equal(expected.Value.ToList()[0].name, actual.Value.ToList()[0].name);
            Assert.Equal(expected.Value.ToList()[0].email, actual.Value.ToList()[0].email);

            Assert.Equal(expected.Value.ToList()[1].id, actual.Value.ToList()[1].id);
            Assert.Equal(expected.Value.ToList()[1].name, actual.Value.ToList()[1].name);
            Assert.Equal(expected.Value.ToList()[1].email, actual.Value.ToList()[1].email);
        }

        [Fact]
        public void GetCustomer_Succeeds()
        {
            //Arrange
            var customerDto = new CustomerDto
            {
                id = 1,
                name = "J",
                email = "e@gmail.com"
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
            Assert.Equal(expected.Value.id, actual.Value.id);
            Assert.Equal(expected.Value.name, actual.Value.name);
            Assert.Equal(expected.Value.email, actual.Value.email);
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
                name = "",
                email = "eg@gmail.com"
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
                name = "W",
                email = ""
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
                name = "W",
                email = "eg@gmail.com"
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
            Assert.Equal(customer.Value.name, customerDto.name);
            Assert.Equal(customer.Value.email, customerDto.email);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}