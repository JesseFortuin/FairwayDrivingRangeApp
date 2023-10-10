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
        private readonly ICustomerRepository customerRepository;

        public CustomerFacade(IRepository<CustomerInformation> repository,
                              IMapper mapper,
                              ICustomerRepository customerRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.customerRepository = customerRepository;
        }

        public ApiResponseDto<bool> AddCustomer(AddCustomerDto customerDto) 
        { 
            if (customerDto == null || 
                string.IsNullOrWhiteSpace(customerDto.Name) ||
                string.IsNullOrWhiteSpace(customerDto.Email))
            {
                return ApiResponseDto<bool>.Error("Invalid Customer Object");
            }

            var customer = mapper.Map<CustomerInformation>(customerDto);

            var result = repository.Add(customer);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteCustomer(int customerId)
        {
            if (customerId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Id");
            }

            var customer = repository.GetById(customerId);

            if (customer == null)
            {
                return ApiResponseDto<bool>.Error("Customer Not Found");
            }

            var result = repository.Delete(customer);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<CustomerDto> GetCustomerById(int customerId)
        {
            if (customerId <= 0) 
            {
                return ApiResponseDto<CustomerDto>.Error("Invalid Id");
            }

            var result = repository.GetById(customerId);

            if (result == null)
            {
                return ApiResponseDto<CustomerDto>.Error("Customer Not Found");
            }

            var customerDto = mapper.Map<CustomerDto>(result);

            return new ApiResponseDto<CustomerDto>(customerDto);
        }

        public ApiResponseDto<CustomerDto> GetCustomerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return ApiResponseDto<CustomerDto>.Error("You were not found on the database");
            }

            var result = customerRepository.GetCustomerByEmail(email);

            if (result == null)
            {
                return ApiResponseDto<CustomerDto>.Error("You were not found on the database");
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
                return ApiResponseDto<bool>.Error("Invalid Id");
            }

            var customer = repository.GetById(customerId);

            if (customer == null)
            {
                return ApiResponseDto<bool>.Error("Customer Not Found");
            }

            if (customerDto == null ||
                string.IsNullOrWhiteSpace(customerDto.Name) ||
                string.IsNullOrWhiteSpace(customerDto.Email))
            {
                return ApiResponseDto<bool>.Error("Invalid Customer Object");
            }

            mapper.Map(customerDto, customer);

            var result = repository.Update(customer);

            return new ApiResponseDto<bool>(result);
        }
    }
}