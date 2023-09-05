using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly FairwayContext fairwayContext;

        public Repository(FairwayContext fairwayContext)
        {
            this.fairwayContext = fairwayContext;
        }

        public bool Create(T entity)
        {
            fairwayContext.Set<T>().Add(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public bool Delete(T entity)
        {
            fairwayContext.Set<T>().Remove(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return fairwayContext.Set<T>().AsEnumerable();
        }

        public T GetById(int id)
        {
            return fairwayContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(T entity)
        {
            fairwayContext.Set<T>().Update(entity);

            fairwayContext.SaveChanges();

            return true;
        }
    }
}
