using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly IRepository<CustomerInformation> repository;

        public CustomerFacade(IRepository<CustomerInformation> repository)
        {
            this.repository = repository;
        }

        public ApiResponseDto<bool> AddCustomer(AddCustomerDto customerDto) 
        { 
            if (customerDto == null || 
                string.IsNullOrWhiteSpace(customerDto.name) ||
                string.IsNullOrWhiteSpace(customerDto.email))
            {
                return new ApiResponseDto<bool>("Invalid Customer Object");
            }

            var customer = new CustomerInformation
            {
                Name = customerDto.name,
                Email = customerDto.email,
                IsPaid = customerDto.isPaid,
            };

            var result = repository.Create(customer);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteCustomer(int customerId)
        {
            if (customerId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Id");
            }

            var customer = repository.GetById(customerId);

            if (customer == null)
            {
                return new ApiResponseDto<bool>("Customer Not Found");
            }

            var result = repository.Delete(customer);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<CustomerDto> GetCustomerById(int customerId)
        {
            if (customerId <= 0) 
            {
                return new ApiResponseDto<CustomerDto>("Invalid Id");
            }

            var result = repository.GetById(customerId);

            if (result == null)
            {
                return new ApiResponseDto<CustomerDto>("Customer Not Found");
            }

            var customerDto = new CustomerDto
            {
                id = result.Id,
                name = result.Name,
                email = result.Email,
                isPaid = result.IsPaid
            };

            return new ApiResponseDto<CustomerDto>(customerDto);
        }

        public ApiResponseDto<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customerDtos = new List<CustomerDto>();

            var customers = repository.GetAll();

            foreach (var customer in customers)
            {
                var customerDto = new CustomerDto
                {
                    name = customer.Name,
                    email = customer.Email,
                    id = customer.Id,
                    isPaid = customer.IsPaid
                };

                customerDtos.Add(customerDto);
            }

            return new ApiResponseDto<IEnumerable<CustomerDto>>(customerDtos);
        }

        public ApiResponseDto<bool> UpdateCustomer(int customerId, AddCustomerDto customerDto)
        {
            if (customerId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Id");
            }

            var customer = repository.GetById(customerId);

            if (customer == null)
            {
                return new ApiResponseDto<bool>("Customer Not Found");
            }

            if (customerDto == null ||
                string.IsNullOrWhiteSpace(customerDto.name) ||
                string.IsNullOrWhiteSpace(customerDto.email))
            {
                return new ApiResponseDto<bool>("Invalid Customer Object");
            }

            customer.Name = customerDto.name; 

            customer.Email = customerDto.email;

            customer.IsPaid = customerDto.isPaid;

            var result = repository.Update(customer);

            return new ApiResponseDto<bool>(result);
        }
    }
}