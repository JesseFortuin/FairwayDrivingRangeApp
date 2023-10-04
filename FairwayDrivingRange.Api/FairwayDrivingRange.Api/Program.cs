using FairwayDrivingRange.Application;
using FairwayDrivingRange.Application.Services;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Infrastructure.Data;
using FairwayDrivingRange.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
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

builder.Services.AddScoped<ICustomerFacade, CustomerFacade>();

builder.Services.AddScoped<IRepository<CustomerInformation>, Repository<CustomerInformation>>();

builder.Services.AddScoped<IBookingFacade, BookingFacade>();

builder.Services.AddScoped<IRepository<Booking>, Repository<Booking>>();

builder.Services.AddScoped<IGolfClubFacade, GolfClubFacade>();

builder.Services.AddScoped<IRepository<GolfClub>, Repository<GolfClub>>();

builder.Services.AddScoped<ITransactionFacade, TransactionFacade>();

builder.Services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();

builder.Services.AddScoped<IAdminFacade, AdminFacade>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddScoped<ICustomerRepository, CustomerRespository>();

builder.Services.AddScoped<IRepository<Admin>, Repository<Admin>>();

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddDbContext<FairwayContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FairwayConectionString")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("Jwt:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddCors(p =>
{
    p.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
