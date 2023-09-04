using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public interface ICustomerFacade
    {
        public ApiResponseDto<bool> AddCustomer(AddCustomerDto customerDto);

        public ApiResponseDto<bool> UpdateCustomer(AddCustomerDto customerDto);

        public ApiResponseDto<List<CustomerDto>> GetCustomers();
    }
}