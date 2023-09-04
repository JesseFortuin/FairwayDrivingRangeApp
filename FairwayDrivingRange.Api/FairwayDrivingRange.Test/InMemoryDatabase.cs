using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FairwayDrivingRange.Test
{
    public class InMemoryDatabase
    {
        readonly IRepository<CustomerInformation> customerInformationRepository;

        readonly FairwayContext context;

        public InMemoryDatabase()
        {
            context = new FairwayContext(DatabaseSetup().Options);

            customerInformationRepository = new Repository<CustomerInformation>(context);
        }

        public DbContextOptionsBuilder<FairwayContext> DatabaseSetup()
        {
            var builder = new DbContextOptionsBuilder<FairwayContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            return builder;
        }
    }
}
