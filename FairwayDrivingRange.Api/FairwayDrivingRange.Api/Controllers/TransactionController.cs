using FairwayDrivingRange.Application;
using FairwayDrivingRange.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FairwayDrivingRange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionFacade transactionFacade;

        public TransactionController(ITransactionFacade transactionFacade)
        {
            this.transactionFacade = transactionFacade;
        }

        [HttpGet()]
        public ActionResult<ApiResponseDto<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var result = transactionFacade.GetTransactions();

            return Ok(result);
        }

        [HttpGet("{transactionId}")]
        public ActionResult<ApiResponseDto<TransactionDto>> GetTransaction(int transactionId)
        {
            var result = transactionFacade.GetTransactionById(transactionId);

            return Ok(result);
        }

        [HttpPost("add")]
        public ActionResult<ApiResponseDto<bool>> AddTransaction(AddTransactionDto transactionDto)
        {
            var result = transactionFacade.AddTransaction(transactionDto);

            return Ok(result);
        }

        [HttpPut("{transactionId}")]
        public ActionResult<ApiResponseDto<bool>> UpdateTransaction(int transactionId, AddTransactionDto transactionDto)
        {
            var result = transactionFacade.UpdateTransaction(transactionId, transactionDto);

            return Ok(result);
        }

        [HttpDelete("{transactionId}")]
        public ActionResult<ApiResponseDto<bool>> DeleteTransaction(int transactionId)
        {
            var result = transactionFacade.DeleteTransaction(transactionId);

            return Ok(result);
        }
    }
}
