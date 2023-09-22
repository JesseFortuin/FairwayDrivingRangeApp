using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Application
{
    public interface ITransactionFacade
    {
        public ApiResponseDto<bool> AddTransaction(AddTransactionDto transactionDto);

        public ApiResponseDto<bool> UpdateTransaction(int transactionId, AddTransactionDto transactionDto);

        public ApiResponseDto<IEnumerable<TransactionDto>> GetTransactions();

        public ApiResponseDto<TransactionDto> GetTransactionById(int transactionId);

        public ApiResponseDto<bool> DeleteTransaction(int transactionId);
    }
}
