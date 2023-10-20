using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public class BookingFacade : IBookingFacade
    {
        private readonly IRepository<Booking> bookingRepository;
        private readonly ICustomerRepository iCustomerRepository;
        private readonly IRepository<GolfClub> golfClubRepository;
        private readonly IMapper mapper;

        public BookingFacade(IRepository<Booking> repository,
                            ICustomerRepository iCustomerRepository,
                            IRepository<GolfClub> golfClubRepository,
                            IMapper mapper)
        {
            this.bookingRepository = repository;
            this.iCustomerRepository = iCustomerRepository;
            this.golfClubRepository = golfClubRepository;
            this.mapper = mapper;
        }

        public ApiResponseDto<bool> AddBooking(AddBookingDto bookingDto)
        {
            //if (bookingDto.lane <= 0 ||
            //    bookingDto.lane > 10)
            //{
            //    return ApiResponseDto<bool>.Error("Invalid Booking Object");
            //}

            if (string.IsNullOrWhiteSpace(bookingDto.Name) ||
                string.IsNullOrWhiteSpace(bookingDto.Email) ||
                string.IsNullOrWhiteSpace(bookingDto.Phone))
            {
                return ApiResponseDto<bool>.Error("Invalid Customer Object");
            }

            //var customer = new CustomerInformation
            //{
            //    Name = bookingDto.name,
            //    Email = bookingDto.email,
            //    Phone = bookingDto.phone
            //};

            var customer = mapper.Map<CustomerInformation>(bookingDto);

            //var customerResult = customerRepository.Create(customer);

            //if (customerResult == false)
            //{
            //    return ApiResponseDto<bool>.Error("Customer Could Not Be Added");
            //};

            var booking = mapper.Map<Booking>(bookingDto);

            booking.Customer = customer;

            var golfClubs = new List<GolfClub>();

            if (bookingDto.GolfClubIds.Length > 0) 
            {
                foreach (var golfClubId in bookingDto.GolfClubIds)
                {
                    var golfClub = golfClubRepository.GetById(golfClubId);

                    golfClubs.Add(golfClub);
                }
            }

            golfClubRepository.UpdateAll(golfClubs.ToArray());

            booking.Clubs = golfClubs;

            try
            {
                var result = bookingRepository.Add(booking);

                return new ApiResponseDto<bool>(result);
            }
            catch (Exception ex)
            {

                return ApiResponseDto<bool>.Error(ex.Message);
            }
        }

        public ApiResponseDto<bool> AddBookingEmail(AddBookingEmailDto bookingDto)
        {
            if (string.IsNullOrWhiteSpace(bookingDto.Email))
            {
                return ApiResponseDto<bool>.Error("Invalid Email");
            }

            var customer = iCustomerRepository.GetCustomerByEmail(bookingDto.Email);

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

            var result = bookingRepository.Add(booking);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> DeleteBooking(int bookingId)
        {
            if (bookingId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Id");
            }

            var booking = bookingRepository.GetById(bookingId);

            if (booking == null)
            {
                return ApiResponseDto<bool>.Error("Booking Not Found");
            }

            try
            {
                booking.IsCancelled = true;

                var result = bookingRepository.Update(booking);

                return new ApiResponseDto<bool>(result);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.Error(ex.Message);
            }
        }

        public ApiResponseDto<BookingDto> GetBookingById(int bookingId)
        {
            if (bookingId <= 0)
            {
                return ApiResponseDto<BookingDto>.Error("Invalid Id");
            }

            var booking = bookingRepository.GetById(bookingId);

            if (booking == null)
            {
                return ApiResponseDto<BookingDto>.Error("Booking Not Found");
            }

            var bookingDto = mapper.Map<BookingDto>(booking);

            return new ApiResponseDto<BookingDto>(bookingDto);
        }

        public ApiResponseDto<IEnumerable<BookingDto>> GetBookings()
        {
            var bookings = bookingRepository.GetAll();

            var bookingDtos = mapper.Map<IEnumerable<BookingDto>>(bookings);

            return new ApiResponseDto<IEnumerable<BookingDto>>(bookingDtos);
        }

        public ApiResponseDto<bool> UpdateBooking(int bookingId, UpdateBookingDto bookingDto)
        {
            if (bookingId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Id");
            }

            var booking = bookingRepository.GetById(bookingId);

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

            if (bookingDto.CustomerId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Booking Object");
            }

            if (bookingDto.CustomerId <= 0)
            {
                return ApiResponseDto<bool>.Error("Invalid Customer Id");
            }

            var customer = bookingRepository.GetById(bookingDto.CustomerId);

            if (customer == null ||
                customer.Customer == null)
            {
                return ApiResponseDto<bool>.Error("Customer Not Found");
            }

            mapper.Map(bookingDto, booking);

            try
            {
                var result = bookingRepository.Update(booking);

                return new ApiResponseDto<bool>(result);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.Error(ex.Message);
            }
        }
    }
}
