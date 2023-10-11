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
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Authorization header using bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

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

            services.AddCors(p =>
            {
                p.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}
