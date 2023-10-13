using FairwayDrivingRange.Application;
using FairwayDrivingRange.Application.Services;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FairwayDrivingRange.Api.Config
{
    public static class IoCModule
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ICustomerFacade, CustomerFacade>();

            services.AddScoped<IRepository<CustomerInformation>, Repository<CustomerInformation>>();

            services.AddScoped<IBookingFacade, BookingFacade>();

            services.AddScoped<IRepository<Booking>, Repository<Booking>>();

            services.AddScoped<IGolfClubFacade, GolfClubFacade>();

            services.AddScoped<IRepository<GolfClub>, Repository<GolfClub>>();

            services.AddScoped<ITransactionFacade, TransactionFacade>();

            services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();

            services.AddScoped<IAdminFacade, AdminFacade>();

            services.AddScoped<IAdminRepository, AdminRepository>();

            services.AddScoped<ICustomerRepository, CustomerRespository>();

            services.AddScoped<IRepository<Admin>, Repository<Admin>>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}
