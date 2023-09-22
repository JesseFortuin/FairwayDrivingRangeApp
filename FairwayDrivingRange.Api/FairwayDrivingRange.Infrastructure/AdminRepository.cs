using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure.Data;

namespace FairwayDrivingRange.Infrastructure
{
    public class AdminRepository : IAdminRepository
    {
        private readonly FairwayContext context;

        public AdminRepository(FairwayContext context)
        {
            this.context = context;
        }

        public Admin GetAdminByName(string name)
        {
            return context.Admins.FirstOrDefault(a => a.AdminName == name);
        }
    }
}
