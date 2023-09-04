using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairwayDrivingRange.Infrastructure
{
    public interface ISpecification<in T>
    {
        bool IsSatisfied(T obj);


    }
}
