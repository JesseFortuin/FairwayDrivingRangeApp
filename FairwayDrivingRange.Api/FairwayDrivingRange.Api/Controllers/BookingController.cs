using FairwayDrivingRange.Application;
using FairwayDrivingRange.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FairwayDrivingRange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingFacade bookingFacade;

        public BookingController(IBookingFacade bookingFacade)
        {
            this.bookingFacade = bookingFacade;
        }

        [HttpGet()]
        public ActionResult<ApiResponseDto<IEnumerable<BookingDto>>> GetBookings()
        {
            var result = bookingFacade.GetBookings();

            return Ok(result);
        }

        [HttpGet("{bookingId}")]
        public ActionResult<ApiResponseDto<BookingDto>> GetBooking(int bookingId)
        {
            var result = bookingFacade.GetBookingById(bookingId);

            return Ok(result);
        }

        [HttpPost("add")]
        public ActionResult<ApiResponseDto<bool>> AddBooking(AddBookingDto bookingDto)
        {
            var result = bookingFacade.AddBooking(bookingDto);

            return Ok(result);
        }

        [HttpPut("{bookingId}")]
        public ActionResult<ApiResponseDto<bool>> UpdateBooking(int bookingId, AddBookingDto bookingDto)
        {
            var result = bookingFacade.UpdateBooking(bookingId, bookingDto);

            return Ok(result);
        }

        [HttpDelete("{bookingId}")]
        public ActionResult<ApiResponseDto<bool>> DeleteBooking(int bookingId)
        {
            var result = bookingFacade.DeleteBooking(bookingId);

            return Ok(result);
        }
    }
}
