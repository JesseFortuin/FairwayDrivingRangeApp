using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public class BookingFacade : IBookingFacade
    {
        private readonly IRepository<Booking> repository;
        private readonly IRepository<CustomerInformation> customerRepository;
        private readonly IMapper mapper;

        public BookingFacade(IRepository<Booking> repository,
                            IRepository<CustomerInformation> customerRepository,
                                 IMapper mapper)
        {
            this.repository = repository;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public ApiResponseDto<bool> AddBooking(AddBookingDto bookingDto)
        {
            if (bookingDto.customerId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Customer Id");
            }

            var customer = customerRepository.GetById(bookingDto.customerId);

            if (customer == null)
            {
                return new ApiResponseDto<bool>("Customer Not Found");
            }

            if (bookingDto.lane <= 0 ||
                bookingDto.lane > 20)
            {
                return new ApiResponseDto<bool>("Invalid Booking Object");
            }

            var booking = mapper.Map<Booking>(bookingDto);

            var result = repository.Create(booking);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteBooking(int bookingId)
        {
            if (bookingId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Id");
            }

            var booking = repository.GetById(bookingId);

            if (booking == null)
            {
                return new ApiResponseDto<bool>("Booking Not Found");
            }

            var result = repository.Delete(booking);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<BookingDto> GetBookingById(int bookingId)
        {
            if (bookingId <= 0)
            {
                return new ApiResponseDto<BookingDto>("Invalid Id");
            }

            var booking = repository.GetById(bookingId);

            if (booking == null)
            {
                return new ApiResponseDto<BookingDto>("Booking Not Found");
            }

            var bookingDto = mapper.Map<BookingDto>(booking);

            return new ApiResponseDto<BookingDto>(bookingDto);
        }

        public ApiResponseDto<IEnumerable<BookingDto>> GetBookings()
        {
            var bookings = repository.GetAll();

            var bookingDtos = mapper.Map<IEnumerable<BookingDto>>(bookings);

            return new ApiResponseDto<IEnumerable<BookingDto>>(bookingDtos);
        }

        public ApiResponseDto<bool> UpdateBooking(int bookingId, AddBookingDto bookingDto)
        {
            if (bookingId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Id");
            }

            var booking = repository.GetById(bookingId);

            if (booking == null)
            {
                return new ApiResponseDto<bool>("Booking Not Found");
            }

            if (bookingDto.lane <= 0 ||
                bookingDto.lane > 20 ||
                bookingDto.customerId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Booking Object");
            }

            if (bookingDto.customerId <= 0)
            {
                return new ApiResponseDto<bool>("Invalid Customer Id");
            }

            var customer = repository.GetById(bookingDto.customerId);

            if (customer == null ||
                customer.Customer == null)
            {
                return new ApiResponseDto<bool>("Customer Not Found");
            }

            mapper.Map(bookingDto, booking);

            var result = repository.Update(booking);

            return new ApiResponseDto<bool>(result);
        }
    }
}
