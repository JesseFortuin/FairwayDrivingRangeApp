using FairwayDrivingRange.Application;
using FairwayDrivingRange.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FairwayDrivingRange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GolfClubController : ControllerBase
    {
        private readonly IGolfClubFacade golfClubFacade;

        public GolfClubController(IGolfClubFacade golfClubFacade)
        {
            this.golfClubFacade = golfClubFacade;
        }

        [HttpGet()]
        public ActionResult<ApiResponseDto<IEnumerable<GolfClubDto>>> GetGolfClubs()
        {
            var result = golfClubFacade.GetGolfClubs();

            return Ok(result);
        }

        [HttpGet("{golfClubId}")]
        public ActionResult<ApiResponseDto<GolfClubDto>> GetGolfClub(int golfClubId)
        {
            var result = golfClubFacade.GetGolfClubById(golfClubId);

            return Ok(result);
        }

        [HttpPost("add")]
        public ActionResult<ApiResponseDto<bool>> AddGolfClub(AddGolfClubDto golfClubDto)
        {
            var result = golfClubFacade.AddGolfClub(golfClubDto);

            return Ok(result);
        }

        [HttpPut("{golfClubId}")]
        public ActionResult<ApiResponseDto<bool>> UpdateGolfClub(int golfClubId, UpdateGolfClubDto golfClubDto)
        {
            var result = golfClubFacade.UpdateGolfClub(golfClubId, golfClubDto);

            return Ok(result);
        }

        [HttpDelete("{golfClubId}")]
        public ActionResult<ApiResponseDto<bool>> DeleteGolfClub(int golfClubId)
        {
            var result = golfClubFacade.DeleteGolfClub(golfClubId);

            return Ok(result);
        }
    }
}
