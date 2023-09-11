using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly IRepository<CustomerInformation> repository;
        private readonly IMapper mapper;

        public CustomerFacade(IRepository<CustomerInformation> repository,
                                 IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public ApiResponseDto<bool> AddCustomer(AddCustomerDto customerDto) 
        { 
            if (customerDto == null || 
                string.IsNullOrWhiteSpace(customerDto.name) ||
                string.IsNullOrWhiteSpace(customerDto.email))
            {
                return new ApiResponseDto<bool>("Invalid Customer Object");
            }

            var customer = mapper.Map<CustomerInformation>(customerDto);

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

            var customerDto = mapper.Map<CustomerDto>(result);

            return new ApiResponseDto<CustomerDto>(customerDto);
        }

        public ApiResponseDto<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customers = repository.GetAll();

            var customerDtos = mapper.Map<IEnumerable<CustomerDto>>(customers); ;

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

            mapper.Map(customerDto, customer);

            var result = repository.Update(customer);

            return new ApiResponseDto<bool>(result);
        }
    }
}