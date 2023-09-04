using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure
{
    public class CustomerRepository : ICustomerRepository, IRepository<CustomerInformation>
    {
        private readonly FairwayContext fairwayContext;

        public CustomerRepository(FairwayContext fairwayContext)
        {
            this.fairwayContext = fairwayContext;
        }

        public bool Create(CustomerInformation entity)
        {
            fairwayContext.CustomerInformation.Add(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public bool Delete(CustomerInformation entity)
        {
            fairwayContext.CustomerInformation.Remove(entity);

            fairwayContext.SaveChanges();

            return true;
        }

        public List<CustomerInformation> GetAll()
        {
            return fairwayContext.CustomerInformation.ToList();
        }

        public CustomerInformation GetById(int id)
        {
            return fairwayContext.CustomerInformation.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(CustomerInformation entity)
        {
            fairwayContext.CustomerInformation.Update(entity);

            fairwayContext.SaveChanges();

            return true;
        }
    }
}
