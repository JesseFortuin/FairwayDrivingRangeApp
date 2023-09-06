using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Application
{
    public class TransactionFacade : ITransactionFacade
    {
        private readonly IRepository<Transaction> repository;

        public TransactionFacade(IRepository<Transaction> repository)
        {
            this.repository = repository;
        }

        public ApiResponseDto<bool> AddTransaction(AddTransactionDto transactionDto)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<bool> DeleteTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<TransactionDto> GetTransactionById(int transactionId)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<IEnumerable<TransactionDto>> GetTransactions()
        {
            throw new NotImplementedException();
        }

        public ApiResponseDto<bool> UpdateTransaction(int transactionId, AddTransactionDto transactionDto)
        {
            throw new NotImplementedException();
        }
    }
}
