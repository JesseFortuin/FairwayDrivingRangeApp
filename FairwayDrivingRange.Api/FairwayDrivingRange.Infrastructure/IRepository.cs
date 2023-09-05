using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure
{
    public interface IRepository<TEntity>
    {
        public IEnumerable<TEntity> GetAll();

        public TEntity GetById(int id);

        public bool Create(TEntity entity);

        public bool Update(TEntity entity);

        public bool Delete(TEntity entity);
    }
}
