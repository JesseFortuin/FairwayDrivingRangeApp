using AutoMapper;
using FairwayDrivingRange.Application;
using FairwayDrivingRange.Application.Services;
using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure;
using FairwayDrivingRange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FairwayDrivingRange.Test
{
    public class BaseTestSetup
    {
        public ICustomerRepository customerRepos;

        public IRepository<CustomerInformation> customerInformationRepository;

        public ICustomerFacade customerFacade;

        public IRepository<Booking> bookingRepository;

        public IBookingFacade bookingFacade;

        public IRepository<Admin> adminRepository;

        public IAdminRepository adminRepos;

        public IAuthenticationService authenticationService;

        public IAdminFacade adminFacade;

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

            var inMemorySettings = new Dictionary<string, string>
            {
                {"Jwt:Token", "Keeping this here for now. It's a secret" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var mapper = mapperConfiguration.CreateMapper();

            context = new FairwayContext(DatabaseSetup().Options);

            customerRepos = new CustomerRespository(context);

            customerInformationRepository = new Repository<CustomerInformation>(context);

            customerFacade = new CustomerFacade(customerInformationRepository, mapper, customerRepos);

            bookingRepository = new Repository<Booking>(context);

            bookingFacade = new BookingFacade(bookingRepository, customerRepos, mapper);

            adminRepository = new Repository<Admin>(context);

            adminRepos = new AdminRepository(context);

            authenticationService = new AuthenticationService(configuration);

            adminFacade = new AdminFacade(adminRepository, adminRepos, mapper, authenticationService);

            golfClubRepository = new Repository<GolfClub>(context);

            golfClubFacade = new GolfClubFacade(golfClubRepository, bookingRepository, mapper);

            transactionRepository = new Repository<Transaction>(context);

            transactionFacade = new TransactionFacade(transactionRepository, bookingRepository, mapper);
        }

        public DbContextOptionsBuilder<FairwayContext> DatabaseSetup()
        {
            var builder = new DbContextOptionsBuilder<FairwayContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            return builder;
        }
    }
}
