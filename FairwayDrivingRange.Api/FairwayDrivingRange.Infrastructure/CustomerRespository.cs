using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure.Data;

namespace FairwayDrivingRange.Infrastructure
{
    public class CustomerRespository : ICustomerRepository
    {
        private readonly FairwayContext context;

        public CustomerRespository(FairwayContext context)
        {
            this.context = context;
        }

        public CustomerInformation GetCustomerByEmail (string email)
        {
            return context.CustomerInformation.FirstOrDefault(c => c.Email == email);
        }
    }
}
