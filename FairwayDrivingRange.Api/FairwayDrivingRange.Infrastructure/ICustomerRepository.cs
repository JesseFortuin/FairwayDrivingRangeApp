using FairwayDrivingRange.Domain.Entities;

namespace FairwayDrivingRange.Infrastructure
{
    public interface ICustomerRepository
    {
        public CustomerInformation GetCustomerByEmail(string email);
    }
}
