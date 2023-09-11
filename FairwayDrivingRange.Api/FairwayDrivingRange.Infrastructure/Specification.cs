using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TODO: implement specification pattern http://specification.ardalis.com/extensions/extend-define-evaluators.html
namespace FairwayDrivingRange.Infrastructure
{
    public class Specification <T>
    {
        public bool IsSatisfied(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
