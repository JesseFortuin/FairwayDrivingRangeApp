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
        private readonly ICustomerRepository iCustomerRepository;
        private readonly IMapper mapper;

        public BookingFacade(IRepository<Booking> repository,
                            IRepository<CustomerInformation> customerRepository,
                            ICustomerRepository iCustomerRepository,
                            IMapper mapper)
        {
            this.repository = repository;
            this.customerRepository = customerRepository;
            this.iCustomerRepository = iCustomerRepository;
            this.mapper = mapper;
        }

        public ApiResponseDto<bool> AddBooking(AddBookingDto bookingDto)
        {
            if (bookingDto.customerId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Customer Id");
            }

            var customer = customerRepository.GetById(bookingDto.customerId);

            if (customer == null)
            {
                return ApiResponseDto<bool>.Error("Customer Not Found");
            }

            //if (bookingDto.lane <= 0 ||
            //    bookingDto.lane > 10)
            //{
            //    return ApiResponseDto<bool>.Error("Invalid Booking Object");
            //}

            var booking = mapper.Map<Booking>(bookingDto);

            var result = repository.Create(booking);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> AddBookingEmail(AddBookingEmailDto bookingDto)
        {
            if (string.IsNullOrWhiteSpace(bookingDto.email))
            {
                return ApiResponseDto<bool>.Error("Invalid Email");
            }

            var customer = iCustomerRepository.GetCustomerByEmail(bookingDto.email);

            if (customer == null)
            {
                return ApiResponseDto<bool>.Error("Customer Not Found");
            }

            //if (bookingDto.lane <= 0 ||
            //    bookingDto.lane > 10)
            //{
            //    return ApiResponseDto<bool>.Error("Invalid Booking Object");
            //}

            var booking = mapper.Map<Booking>(bookingDto);

            var result = repository.Create(booking);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteBooking(int bookingId)
        {
            if (bookingId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Id");
            }

            var booking = repository.GetById(bookingId);

            if (booking == null)
            {
                return ApiResponseDto<bool>.Error("Booking Not Found");
            }

            var result = repository.Delete(booking);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<BookingDto> GetBookingById(int bookingId)
        {
            if (bookingId <= 0)
            {
                return ApiResponseDto<BookingDto>.Error("Invalid Id");
            }

            var booking = repository.GetById(bookingId);

            if (booking == null)
            {
                return ApiResponseDto<BookingDto>.Error("Booking Not Found");
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
                return ApiResponseDto<bool>.Error("Invalid Id");
            }

            var booking = repository.GetById(bookingId);

            if (booking == null)
            {
                return ApiResponseDto<bool>.Error("Booking Not Found");
            }

            //if (bookingDto.lane <= 0 ||
            //    bookingDto.lane > 20 ||
            //    bookingDto.customerId <= 0)
            //{
            //    return ApiResponseDto<bool>.Error("Invalid Booking Object");
            //}

            if (bookingDto.customerId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Booking Object");
            }

            if (bookingDto.customerId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Customer Id");
            }

            var customer = repository.GetById(bookingDto.customerId);

            if (customer == null ||
                customer.Customer == null)
            {
                return ApiResponseDto<bool>.Error("Customer Not Found");
            }

            mapper.Map(bookingDto, booking);

            var result = repository.Update(booking);

            return new ApiResponseDto<bool>(result);
        }
    }
}
