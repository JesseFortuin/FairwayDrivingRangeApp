using FairwayDrivingRange.Application;
using FairwayDrivingRange.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FairwayDrivingRange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAdminFacade adminFacade;

        public AuthenticationController(IAdminFacade adminFacade)
        {
            this.adminFacade = adminFacade;
        }

        [HttpPost("Register")]
        public ActionResult<ApiResponseDto<bool>> Register(AdminDto adminDto)
        {
            var result = adminFacade.RegisterAdmin(adminDto);

            return Ok(result);
        }

        [HttpPost("Login")]
        public ActionResult<ApiResponseDto<string>> Login(AdminDto adminDto)
        {
            var result = adminFacade.Login(adminDto);

            return Ok(result);
        }
    }
}
