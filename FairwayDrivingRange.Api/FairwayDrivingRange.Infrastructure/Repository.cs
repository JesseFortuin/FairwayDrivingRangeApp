using FairwayDrivingRange.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
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
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
