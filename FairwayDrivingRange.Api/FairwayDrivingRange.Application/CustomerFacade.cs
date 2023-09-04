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
                return new ApiResponseDto<bool>("Invalid Customer object");
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

        public ApiResponseDto<List<CustomerDto>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<bool> UpdateCustomer(AddCustomerDto customerDto)
        {
            throw new NotImplementedException();
        }
    }
}