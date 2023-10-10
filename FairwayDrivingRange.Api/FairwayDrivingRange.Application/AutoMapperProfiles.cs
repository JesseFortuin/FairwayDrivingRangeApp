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

            CreateMap<Booking, BookingDto>();

            CreateMap<AddBookingDto, Booking>();

            CreateMap<UpdateBookingDto, Booking>();

            CreateMap<AddBookingEmailDto, Booking>();

            CreateMap<Admin, AdminDto>().ReverseMap();

            CreateMap<AddBookingDto, CustomerInformation>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}
