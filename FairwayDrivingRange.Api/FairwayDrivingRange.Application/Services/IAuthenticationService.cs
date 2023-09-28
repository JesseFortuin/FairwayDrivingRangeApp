using FairwayDrivingRange.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Application.Services
{
    public interface IAuthenticationService
    {
        public string GenerateToken(string adminName);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(string adminName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, adminName)
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

            return jwt;
        }
    }
}
