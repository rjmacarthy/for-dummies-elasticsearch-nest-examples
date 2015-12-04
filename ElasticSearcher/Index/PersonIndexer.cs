using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using ElasticSearcher.Models;
using ElasticSearcher.Searches;

namespace ElasticSearcher.Index
{
    class PersonIndexer
    {
        public static void IndexPerson(IElasticClient client)
        {
            Person person = new Person
            {
                Id = 2,
                Name = "Adam",
                Job = "Developer"
            };

            client.Index(person);

        }

        public static bool CheckIndexed (int id, IElasticClient client)
        {
            var idList = new List<string> { id.ToString() };

            var result = client.Search<Person>(s => s.AllTypes().Query(p => p.Ids(idList)));

            return !result.Documents.Any();

        }
    }
}
