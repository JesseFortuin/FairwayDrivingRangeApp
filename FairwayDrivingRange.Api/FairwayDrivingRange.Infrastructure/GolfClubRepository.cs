using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure
{
    public class GolfClubRepository : IGolfClubRepository, IRepository<GolfClub>
    {
        private readonly FairwayContext fairwayContext;

        public GolfClubRepository(FairwayContext fairwayContext)
        {
            this.fairwayContext = fairwayContext;
        }

        public bool Create(GolfClub entity)
        {
            fairwayContext.GolfClubs.Add(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public bool Delete(GolfClub entity)
        {
            fairwayContext.GolfClubs.Remove(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public List<GolfClub> GetAll()
        {
            return fairwayContext.GolfClubs.ToList();
        }

        public GolfClub GetById(int id)
        {
            return fairwayContext.GolfClubs.FirstOrDefault(x => x.SerialNumber == id);
        }

        public bool Update(GolfClub entity)
        {
            fairwayContext.GolfClubs.Update(entity);

            fairwayContext.SaveChanges();

            return true;
        }
    }
}
