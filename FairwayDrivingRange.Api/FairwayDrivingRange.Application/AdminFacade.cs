using AutoMapper;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FairwayDrivingRange.Application
{
    public class AdminFacade : IAdminFacade
    {
        private readonly IRepository<Admin> repository;
        private readonly IAdminRepository adminRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AdminFacade(IRepository<Admin> repository,
                           IAdminRepository adminRepository,
                           IMapper mapper,
                           IConfiguration configuration)
        {
            this.repository = repository;
            this.adminRepository = adminRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public ApiResponseDto<string> Login(AdminDto adminDto)
        {
            if (adminDto == null)
            {
                return ApiResponseDto<string>.Error("Insert Employee Details");
            }

            if (string.IsNullOrWhiteSpace(adminDto.adminName) ||
                string.IsNullOrWhiteSpace(adminDto.password))
            {
                return ApiResponseDto<string>.Error("Insert Valid Details");
            }

            var admin = adminRepository.GetAdminByName(adminDto.adminName);

            if (admin == null)
            {
                return ApiResponseDto<string>.Error("Invalid Username Or Password");
            }

            if (!BCrypt.Net.BCrypt.Verify(adminDto.password, admin.Password))
            {
                return ApiResponseDto<string>.Error("Invalid Username Or Password");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.AdminName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration
                .GetSection("Jwt:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds,
                claims: claims
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new ApiResponseDto<string>(jwt);
        }

        public ApiResponseDto<bool> RegisterAdmin(AdminDto adminDto)
        {
            if (adminDto == null)
            {
                return ApiResponseDto<bool>.Error("Insert Employee Details");
            }

            if (string.IsNullOrWhiteSpace(adminDto.adminName) ||
                string.IsNullOrWhiteSpace(adminDto.password))
            {
                return ApiResponseDto<bool>.Error("Insert Valid Details");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(adminDto.password);

            adminDto.password = passwordHash;

            var admin = mapper.Map<Admin>(adminDto);

            var result = repository.Create(admin);

            return new ApiResponseDto<bool>(result);
        }
    }
}
