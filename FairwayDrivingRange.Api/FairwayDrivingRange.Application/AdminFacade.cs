using AutoMapper;
using FairwayDrivingRange.Application.Services;
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
        private readonly IAuthenticationService authenticationService;

        public AdminFacade(IRepository<Admin> repository,
                           IAdminRepository adminRepository,
                           IMapper mapper,
                           IAuthenticationService authenticationService)
        {
            this.repository = repository;
            this.adminRepository = adminRepository;
            this.mapper = mapper;
            this.authenticationService = authenticationService;
        }

        public ApiResponseDto<string> Login(AdminDto adminDto)
        {
            if (adminDto == null)
            {
                return ApiResponseDto<string>.Error("Insert Employee Details");
            }

            if (string.IsNullOrWhiteSpace(adminDto.AdminName) ||
                string.IsNullOrWhiteSpace(adminDto.Password))
            {
                return ApiResponseDto<string>.Error("Insert Valid Details");
            }

            var admin = adminRepository.GetAdminByName(adminDto.AdminName);

            if (admin == null)
            {
                return ApiResponseDto<string>.Error("Invalid Username Or Password");
            }

            if (!BCrypt.Net.BCrypt.Verify(adminDto.Password, admin.Password))
            {
                return ApiResponseDto<string>.Error("Invalid Username Or Password");
            }

            var jwt = authenticationService.GenerateToken(admin.AdminName);

            return new ApiResponseDto<string>(jwt);
        }

        public ApiResponseDto<bool> RegisterAdmin(AdminDto adminDto)
        {
            if (adminDto == null)
            {
                return ApiResponseDto<bool>.Error("Insert Employee Details");
            }

            if (string.IsNullOrWhiteSpace(adminDto.AdminName) ||
                string.IsNullOrWhiteSpace(adminDto.Password))
            {
                return ApiResponseDto<bool>.Error("Insert Valid Details");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(adminDto.Password);

            adminDto.Password = passwordHash;

            var admin = mapper.Map<Admin>(adminDto);

            var result = repository.Add(admin);

            return new ApiResponseDto<bool>(result);
        }
    }
}
