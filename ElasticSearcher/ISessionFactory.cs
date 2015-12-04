using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearcher
{
    public interface INhibernateSessionFactory
    {
        T Add<T>(T entity);
        T Get<T>(int id);
        T Delete<T>(int id);
        T Update<T>(int id, T entity);
    }
}
