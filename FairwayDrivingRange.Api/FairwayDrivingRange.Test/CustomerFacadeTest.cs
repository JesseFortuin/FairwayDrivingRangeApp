using FairwayDrivingRange.Application;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure.Data;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Test
{
    public class CustomerFacadeTest : InMemoryDatabase
    {
        [Fact]
        public void AddCustomer_Fails_InvalidNameCustomerDto()
        {
            using (var context = new FairwayContext(DatabaseSetup().Options))
            {
                //Arrange
                ICustomerFacade customerFacade = new CustomerFacade(null);

                var customerDto = new AddCustomerDto
                {
                    name = "",
                    email = "thisisanEmail@email.com",
                    isPaid = false
                };

                var expected = new ApiResponseDto<bool>("Invalid Customer object");

                //Act
                var actual = customerFacade.AddCustomer(customerDto);

                //Assert
                Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
            }
        }

        [Fact]
        public void AddCustomer_Fails_InvalidEmailCustomerDto()
        {
            using (var context = new FairwayContext(DatabaseSetup().Options))
            {
                //Arrange
                ICustomerFacade customerFacade = new CustomerFacade(null);

                var customerDto = new AddCustomerDto
                {
                    name = "Jane Doe",
                    email = "",
                    isPaid = false
                };

                var expected = new ApiResponseDto<bool>("Invalid Customer object");

                //Act
                var actual = customerFacade.AddCustomer(customerDto);

                //Assert
                Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
            }
        }

        [Fact]
        public void AddCustomer_Succeeds_ValidCustomerDto()
        {
            using (var context = new FairwayContext(DatabaseSetup().Options))
            {
                //Arrange               
                IRepository<CustomerInformation> repository = new CustomerRepository(context);

                var customer = new CustomerInformation
                {
                    Name = "Test Name",
                    Email = "thisisanEmail@email.com",
                    IsPaid = false
                };

                context.Add(customer);

                context.SaveChanges();

                ICustomerFacade customerFacade = new CustomerFacade();

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
        }

        //    [Fact]
        //    public void GetCustomers_Succeeds()
        //    {
        //        using (var context = new FairwayContext(DatabaseSetup().Options))
        //        {
        //            //Arrange
        //            ICustomerRepository customerRepository = new CustomerRepository(context);

        //            IRepository<CustomerInformation> crudRepository = new CustomerRepository(context);

        //            var customers = new List<CustomerInformation>
        //            {
        //                new CustomerInformation
        //                {
        //                    Name = "J",
        //                    Email = "e@gmail.com",
        //                    IsPaid = false
        //                },
        //                new CustomerInformation
        //                {
        //                    Name = "k",
        //                    Email = "e@gmail.com",
        //                    IsPaid = true
        //                }
        //            };

        //            var customerDtos = new List<CustomerDto>
        //            {
        //                new CustomerDto
        //                {
        //                    id = 1,
        //                    name = "J",
        //                    email = "e@gmail.com",
        //                    isPaid = false
        //                },
        //                new CustomerDto
        //                {
        //                    id = 2,
        //                    name = "k",
        //                    email = "e@gmail.com",
        //                    isPaid = true
        //                }
        //            };

        //            context.AddRange(customers);

        //            ICustomerFacade customerFacade = new CustomerFacade(customerFacade, crudRepository);

        //            var expected = new ApiResponseDto<List<CustomerDto>>(customerDtos);

        //            //Act
        //            var actual = customerFacade.GetCustomers();

        //            //Assert
        //            Assert.Equal(expected.Value[0].id, actual.Value[0].id);
        //            Assert.Equal(expected.Value[0].name, actual.Value[0].name);
        //            Assert.Equal(expected.Value[0].email, actual.Value[0].email);
        //            Assert.Equal(expected.Value[0].isPaid, actual.Value[0].isPaid);

        //            Assert.Equal(expected.Value[1].id, actual.Value[1].id);
        //            Assert.Equal(expected.Value[1].name, actual.Value[1].name);
        //            Assert.Equal(expected.Value[1].email, actual.Value[1].email);
        //            Assert.Equal(expected.Value[1].isPaid, actual.Value[1].isPaid);
        //        }
        //    }

        //    [Fact]
        //    public void GetCustomer_Succeeds()
        //    {
        //        using (var context = new FairwayContext(DatabaseSetup().Options))
        //        {
        //            //Arrange
        //            ICustomerRepository customerRepository = new CustomerRepository(context);

        //            IRepository<CustomerInformation> crudRepository = new CustomerRepository(context);

        //            var customer = new CustomerInformation
        //            {
        //                Name = "J",
        //                Email = "e@gmail.com",
        //                IsPaid = false
        //            };

        //            var customerDto = new CustomerDto
        //            {
        //                id = 1,
        //                name = "J",
        //                email = "e@gmail.com",
        //                isPaid = false
        //            };

        //            context.AddRange(customer);

        //            ICustomerFacade customerFacade = new CustomerFacade(customerFacade, crudRepository);

        //            var expected = new ApiResponseDto<CustomerDto>(customerDto);

        //            //Act
        //            var actual = customerFacade.GetCustomerbyId(1);

        //            //Assert
        //            Assert.Equal(expected.Value.id, actual.Value.id);
        //            Assert.Equal(expected.Value.name, actual.Value.name);
        //            Assert.Equal(expected.Value.email, actual.Value.email);
        //            Assert.Equal(expected.Value.isPaid, actual.Value.isPaid);
        //        }
        //    }

        //    [Fact]
        //    public void GetCustomer_Fails_IdNegativeNumber()
        //    {
        //        using (var context = new FairwayContext(DatabaseSetup().Options))
        //        {
        //            //Arrange
        //            ICustomerFacade customerFacade = new CustomerFacade(null, null);

        //            var expected = new ApiResponseDto<bool>("Invalid Id");

        //            //Act
        //            var actual = customerFacade.GetCustomerbyId(-1);

        //            //Assert
        //            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        //        }
        //    }

        //    [Fact]
        //    public void GetCustomer_Fails_NumberNotFound()
        //    {
        //        using (var context = new FairwayContext(DatabaseSetup().Options))
        //        {
        //            //Arrange
        //            ICustomerRepository customerRepository = new CustomerRepository(context);

        //            IRepository<CustomerInformation> crudRepository = new CustomerRepository(context);

        //            ICustomerFacade customerFacade = new CustomerFacade(customerFacade, crudRepository);

        //            var expected = new ApiResponseDto<bool>("Customer Not Found");

        //            //Act
        //            var actual = customerFacade.GetCustomerbyId(30);

        //            //Assert
        //            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        //        }
        //    }
        //}
    }

}