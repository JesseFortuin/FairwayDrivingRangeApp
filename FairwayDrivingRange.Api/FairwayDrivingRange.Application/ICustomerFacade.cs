using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public interface ICustomerFacade
    {
        public ApiResponseDto<bool> AddCustomer(AddCustomerDto customerDto);

        public ApiResponseDto<bool> UpdateCustomer(int customerId, AddCustomerDto customerDto);

        public ApiResponseDto<IEnumerable<CustomerDto>> GetCustomers();

        public ApiResponseDto<CustomerDto> GetCustomerById(int customerId);

        public ApiResponseDto<bool> DeleteCustomer(int customerId);

        public ApiResponseDto<CustomerDto> GetCustomerByEmail(string email);
    }
}