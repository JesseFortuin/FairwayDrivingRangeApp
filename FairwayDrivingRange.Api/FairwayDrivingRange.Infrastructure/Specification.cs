using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TODO: implement specification pattern http://specification.ardalis.com/extensions/extend-define-evaluators.html
namespace FairwayDrivingRange.Infrastructure
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public bool IsSatisfied(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
