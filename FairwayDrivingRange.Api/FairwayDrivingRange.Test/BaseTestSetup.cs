using AutoMapper;
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
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new AutoMapperProfiles());
            });

            var mapper = mapperConfiguration.CreateMapper();

            context = new FairwayContext(DatabaseSetup().Options);

            customerInformationRepository = new Repository<CustomerInformation>(context);

            customerFacade = new CustomerFacade(customerInformationRepository, mapper);

            bookingRepository = new Repository<Booking>(context);

            bookingFacade = new BookingFacade(bookingRepository, customerInformationRepository, mapper);

            golfClubRepository = new Repository<GolfClub>(context);

            golfClubFacade = new GolfClubFacade(golfClubRepository, bookingRepository, mapper);

            transactionRepository = new Repository<Transaction>(context);

            transactionFacade = new TransactionFacade(transactionRepository, customerInformationRepository, mapper);
        }

        public DbContextOptionsBuilder<FairwayContext> DatabaseSetup()
        {
            var builder = new DbContextOptionsBuilder<FairwayContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            return builder;
        }
    }
}
