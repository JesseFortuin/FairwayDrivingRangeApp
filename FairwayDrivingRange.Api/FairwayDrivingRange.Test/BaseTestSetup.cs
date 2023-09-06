using FairwayDrivingRange.Application;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FairwayDrivingRange.Test
{
    public class BaseTestSetup
    {
        public IRepository<CustomerInformation> customerInformationRepository;

        public ICustomerFacade customerFacade;

        public IRepository<Booking> bookingRepository;

        public IBookingFacade bookingFacade;

        public IRepository<GolfClub> golfClubRepository;

        public IGolfClubFacade golfClubFacade;

        public IRepository<Transaction> transactionRepository;

        public ITransactionFacade transactionFacade;

        public readonly FairwayContext context;

        public BaseTestSetup()
        {
            context = new FairwayContext(DatabaseSetup().Options);

            customerInformationRepository = new Repository<CustomerInformation>(context);

            customerFacade = new CustomerFacade(customerInformationRepository);

            bookingRepository = new Repository<Booking>(context);

            bookingFacade = new BookingFacade(bookingRepository);

            golfClubRepository = new Repository<GolfClub>(context);

            golfClubFacade = new GolfClubFacade(golfClubRepository);

            transactionRepository = new Repository<Transaction>(context);

            transactionFacade = new TransactionFacade(transactionRepository);
        }

        public DbContextOptionsBuilder<FairwayContext> DatabaseSetup()
        {
            var builder = new DbContextOptionsBuilder<FairwayContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            return builder;
        }
    }
}
