using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public class TransactionFacade : ITransactionFacade
    {
        private readonly IRepository<Transaction> repository;
        private readonly IRepository<CustomerInformation> customerRepository;
        private readonly IMapper mapper;

        public TransactionFacade(IRepository<Transaction> repository,
                                 IRepository<CustomerInformation> customerRepository,
                                 IMapper mapper)
        {
            this.repository = repository;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public ApiResponseDto<bool> AddTransaction(AddTransactionDto transactionDto)
        {
            if (transactionDto.customerId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Customer Id");
            }

            var customer = customerRepository.GetById(transactionDto.customerId);

            if (customer == null)
            {
                return new ApiResponseDto<bool>("Customer Not Found");
            }

            if (transactionDto.bookingPrice <= 0 ||
                transactionDto.total <= 0)
            {
                return new ApiResponseDto<bool>("Booking Price And Total Can Not Be 0");
            }

            if (transactionDto.clubPrice + transactionDto.bookingPrice 
                != transactionDto.total)
            {
                return new ApiResponseDto<bool>("Total Price Does Not Add Up");
            }

            var transaction = mapper.Map<Transaction>(transactionDto);

            var result = repository.Create(transaction);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteTransaction(int transactionId)
        {
            if (transactionId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Id");
            }

            var transaction = repository.GetById(transactionId);

            if (transaction == null)
            {
                return new ApiResponseDto<bool>("Transaction Not Found");
            }

            var result = repository.Delete(transaction);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<TransactionDto> GetTransactionById(int transactionId)
        {
            if (transactionId <= 0)
            {
                return new ApiResponseDto<TransactionDto>("Invalid Id");
            }

            var transaction = repository.GetById(transactionId);

            if (transaction == null)
            {
                return new ApiResponseDto<TransactionDto>("Transaction Not Found");
            }

            var transactionDto = mapper.Map<TransactionDto>(transaction);

            return new ApiResponseDto<TransactionDto>(transactionDto);
        }

        public ApiResponseDto<IEnumerable<TransactionDto>> GetTransactions()
        {
            var transactions = repository.GetAll();

            var transactionDtos = mapper.Map<IEnumerable<TransactionDto>>(transactions);

            return new ApiResponseDto<IEnumerable<TransactionDto>>(transactionDtos);
        }

        public ApiResponseDto<bool> UpdateTransaction(int transactionId, AddTransactionDto transactionDto)
        {
            if (transactionDto.customerId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Customer Id");
            }

            var customer = customerRepository.GetById(transactionDto.customerId);

            if (customer == null)
            {
                return new ApiResponseDto<bool>("Customer Not Found");
            }

            if (transactionDto.bookingPrice <= 0 ||
                transactionDto.total <= 0)
            {
                return new ApiResponseDto<bool>("Booking Price And Total Can Not Be 0");
            }

            if (transactionDto.clubPrice + transactionDto.bookingPrice
                != transactionDto.total)
            {
                return new ApiResponseDto<bool>("Total Price Does Not Add Up");
            }

            if (transactionId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Transaction Id");
            }

            var transaction = repository.GetById(transactionId);

            if (transaction == null)
            {
                return new ApiResponseDto<bool>("Transaction Not Found");
            }

            transaction.CustomerId = transactionDto.customerId;

            transaction.Total = transactionDto.total;

            transaction.ClubPrice = transactionDto.clubPrice;

            transaction.BookingPrice = transactionDto.bookingPrice;

            var result = repository.Update(transaction);

            return new ApiResponseDto<bool>(result);
        }
    }
}
