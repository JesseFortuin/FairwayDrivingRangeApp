using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            CreateMap<Booking, BookingDto>().ReverseMap();

            CreateMap<AddBookingDto, Booking>();
        }
    }
}
