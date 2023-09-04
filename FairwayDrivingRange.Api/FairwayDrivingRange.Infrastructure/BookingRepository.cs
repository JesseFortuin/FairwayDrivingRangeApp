using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure
{
    public class BookingRepository  : IBookingRepository, IRepository<Booking>
    {
        private readonly FairwayContext fairwayContext;

        public BookingRepository(FairwayContext fairwayContext)
        {
            this.fairwayContext = fairwayContext;
        }

        public bool Create(Booking entity)
        {
            fairwayContext.Bookings.Add(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public bool Delete(Booking entity)
        {
            fairwayContext.Bookings.Remove(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public List<Booking> GetAll()
        {
            return fairwayContext.Bookings.ToList();
        }

        public Booking GetById(int id)
        {
            return fairwayContext.Bookings.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(Booking entity)
        {
            fairwayContext.Bookings.Update(entity);

            fairwayContext.SaveChanges();
            
            return true;
        }
    }
}
