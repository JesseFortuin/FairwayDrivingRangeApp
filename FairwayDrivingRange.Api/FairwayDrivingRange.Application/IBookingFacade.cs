using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public interface IBookingFacade
    {
        public ApiResponseDto<bool> AddBooking(AddBookingDto bookingDto);

        public ApiResponseDto<bool> AddBookingEmail(AddBookingEmailDto bookingDto);

        public ApiResponseDto<bool> UpdateBooking(int bookingId, AddBookingDto bookingDto);

        public ApiResponseDto<IEnumerable<BookingDto>> GetBookings();

        public ApiResponseDto<BookingDto> GetBookingById(int bookingId);

        public ApiResponseDto<bool> DeleteBooking(int bookingId);
    }
}
