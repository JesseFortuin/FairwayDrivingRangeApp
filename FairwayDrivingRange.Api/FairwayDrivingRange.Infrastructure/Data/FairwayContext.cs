using FairwayDrivingRange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure.Data
{
    public class FairwayContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<CustomerInformation> CustomerInformation { get; set; }

        public DbSet<GolfClub> GolfClubs { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public FairwayContext(DbContextOptions options) : base(options) { }
    }
}
