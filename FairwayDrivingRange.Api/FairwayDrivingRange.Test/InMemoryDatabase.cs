using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FairwayDrivingRange.Test
{
    public class InMemoryDatabase
    {
        public readonly IRepository<CustomerInformation> customerInformationRepository;

        readonly FairwayContext context;

        public InMemoryDatabase()
        {
            context = new FairwayContext(DatabaseSetup().Options);

            customerInformationRepository = new Repository<CustomerInformation>(context);

            var customers = new List<CustomerInformation>
            {
                new CustomerInformation
                {
                    Name = "J",
                    Email = "e@gmail.com",
                    IsPaid = false
                },
                new CustomerInformation
                {
                    Name = "k",
                    Email = "e@gmail.com",
                    IsPaid = true
                }
            };

            context.CustomerInformation.AddRange(customers);

            context.SaveChanges();
        }

        public DbContextOptionsBuilder<FairwayContext> DatabaseSetup()
        {
            var builder = new DbContextOptionsBuilder<FairwayContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            return builder;
        }
    }
}
