using FairwayDrivingRange.Application;
using FairwayDrivingRange.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FairwayDrivingRange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerFacade customerFacade;

        public CustomerController(ICustomerFacade customerFacade)
        {
            this.customerFacade = customerFacade;
        }

        [HttpGet()]
        public ActionResult<ApiResponseDto<IEnumerable<CustomerDto>>> GetCustomers() 
        { 
            var result = customerFacade.GetCustomers();

            return Ok(result);
        }

        [HttpGet("{customerId}")]
        public ActionResult<ApiResponseDto<CustomerDto>> GetCustomer(int customerId) 
        { 
            var result = customerFacade.GetCustomerById(customerId);

            return Ok(result);
        }

        [HttpPost("add")]
        public ActionResult<ApiResponseDto<bool>> AddCustomer(AddCustomerDto customerDto) 
        {
            var result = customerFacade.AddCustomer(customerDto);

            return Ok(result);
        }

        [HttpPut("{customerId}")]
        public ActionResult<ApiResponseDto<bool>> UpdateCustomer(int customerId, AddCustomerDto customerDto)
        {
            var result = customerFacade.UpdateCustomer(customerId, customerDto);

            return Ok(result);
        }

        [HttpDelete("{customerId}")]
        public ActionResult<ApiResponseDto<bool>> DeleteCustomer(int customerId)
        {
            var result = customerFacade.DeleteCustomer(customerId);

            return Ok(result);
        }
    }
}
