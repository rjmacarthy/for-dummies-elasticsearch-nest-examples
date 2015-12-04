using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearcher.Searches
{
    interface IPersonSearch
    {
        T GetPerson<T>(int id) where T : class;
    }
}
