namespace FairwayDrivingRange.Infrastructure
{
    public interface IRepository<TEntity>
    {
        public IEnumerable<TEntity> GetAll();

        public TEntity GetById(int id);

        public bool Add(TEntity entity);

        public bool AddAll(params TEntity[] entity);

        public bool Update(TEntity entity);

        public bool UpdateAll(params TEntity[] entity);

        public bool Delete(TEntity entity);
    }
}
