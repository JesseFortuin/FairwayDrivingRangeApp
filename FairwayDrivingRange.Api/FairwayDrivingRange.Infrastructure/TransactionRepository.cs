using FairwayDrivingRange.Infrastructure.Data;
using FairwayDrivingRange.Domain.Entities;

namespace FairwayDrivingRange.Infrastructure
{
    public class TransactionRepository : ITransactionRepository, IRepository<Transaction>
    {
        private readonly FairwayContext fairwayContext;

        public TransactionRepository(FairwayContext fairwayContext)
        {
            this.fairwayContext = fairwayContext;
        }

        public bool Create(Transaction entity)
        {
            fairwayContext.Transactions.Add(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public bool Delete(Transaction entity)
        {
            fairwayContext.Transactions.Remove(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public List<Transaction> GetAll()
        {
            return fairwayContext.Transactions.ToList();
        }

        public Transaction GetById(int id)
        {
            return fairwayContext.Transactions.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(Transaction entity)
        {
            fairwayContext.Transactions.Update(entity);

            fairwayContext.SaveChanges();

            return true;
        }
    }
}
