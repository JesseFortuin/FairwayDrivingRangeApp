using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Application
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Transaction, TransactionDto>().ReverseMap();

            CreateMap<AddTransactionDto, Transaction>();

            CreateMap<CustomerInformation, CustomerDto>().ReverseMap();

            CreateMap<AddCustomerDto, CustomerInformation>();

            CreateMap<AddGolfClubDto, GolfClub>();

            CreateMap<GolfClub, GolfClubDto>().ReverseMap();

            CreateMap<UpdateGolfClubDto, GolfClub>();

            CreateMap<Booking, BookingDto>().ForMember(dest => dest.start, opt => opt.MapFrom(src => src.DateBooked)).ReverseMap();

            CreateMap<AddBookingDto, Booking>().ForMember(dest => dest.DateBooked, opt => opt.MapFrom(src => src.start));

            CreateMap<AddBookingEmailDto, Booking>().ForMember(dest => dest.DateBooked, opt => opt.MapFrom(src => src.start));

            CreateMap<Admin, AdminDto>().ReverseMap();
        }
    }
}
