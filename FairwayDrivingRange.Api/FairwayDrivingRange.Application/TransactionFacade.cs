using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public class TransactionFacade : ITransactionFacade
    {
        private readonly IRepository<Transaction> repository;
        private readonly IRepository<Booking> bookingRepository;
        private readonly IMapper mapper;

        public TransactionFacade(IRepository<Transaction> repository,
                                 IRepository<Booking> bookingRepository,
                                 IMapper mapper)
        {
            this.repository = repository;
            this.bookingRepository = bookingRepository;
            this.mapper = mapper;
        }

        public ApiResponseDto<bool> AddTransaction(AddTransactionDto transactionDto)
        {
            if (transactionDto.BookingId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Booking Id");
            }

            var booking = bookingRepository.GetById(transactionDto.BookingId);

            if (booking == null)
            {
                return ApiResponseDto<bool>.Error("Booking Not Found");
            }

            if (transactionDto.BookingPrice <= 0 ||
                transactionDto.Total <= 0)
            {
                return ApiResponseDto<bool>.Error("Booking Price And Total Can Not Be 0");
            }

            if (transactionDto.ClubPrice + transactionDto.BookingPrice 
                != transactionDto.Total)
            {
                return ApiResponseDto<bool>.Error("Total Price Does Not Add Up");
            }

            var transaction = mapper.Map<Transaction>(transactionDto);

            var result = repository.Add(transaction);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteTransaction(int transactionId)
        {
            if (transactionId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Id");
            }

            var transaction = repository.GetById(transactionId);

            if (transaction == null)
            {
                return ApiResponseDto<bool>.Error("Transaction Not Found");
            }

            var result = repository.Delete(transaction);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<TransactionDto> GetTransactionById(int transactionId)
        {
            if (transactionId <= 0)
            {
                return ApiResponseDto<TransactionDto>.Error("Invalid Id");
            }

            var transaction = repository.GetById(transactionId);

            if (transaction == null)
            {
                return ApiResponseDto<TransactionDto>.Error("Transaction Not Found");
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
            if (transactionDto.BookingId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Booking Id");
            }

            var booking = bookingRepository.GetById(transactionDto.BookingId);

            if (booking == null)
            {
                return ApiResponseDto<bool>.Error("Booking Not Found");
            }

            if (transactionDto.BookingPrice <= 0 ||
                transactionDto.Total <= 0)
            {
                return ApiResponseDto<bool>.Error("Booking Price And Total Can Not Be 0");
            }

            if (transactionDto.ClubPrice + transactionDto.BookingPrice
                != transactionDto.Total)
            {
                return ApiResponseDto<bool>.Error("Total Price Does Not Add Up");
            }

            if (transactionId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Transaction Id");
            }

            var transaction = repository.GetById(transactionId);

            if (transaction == null)
            {
                return ApiResponseDto<bool>.Error("Transaction Not Found");
            }

            mapper.Map(transactionDto, transaction);

            var result = repository.Update(transaction);

            return new ApiResponseDto<bool>(result);
        }
    }
}
