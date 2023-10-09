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

            CreateMap<AddBookingDto, AddCustomerDto>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.phone));

            CreateMap<AddBookingDto, CustomerInformation>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.phone));
        }
    }
}
