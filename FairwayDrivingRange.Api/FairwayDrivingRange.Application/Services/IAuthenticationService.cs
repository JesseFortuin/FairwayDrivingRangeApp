namespace FairwayDrivingRange.Application.Services
{
    public interface IAuthenticationService
    {
        public string GenerateToken(string adminName);
    }
}
