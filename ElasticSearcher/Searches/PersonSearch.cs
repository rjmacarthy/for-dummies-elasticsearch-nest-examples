using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticSearcher.Models;
using Nest;

namespace ElasticSearcher.Searches
{
    class PersonSearch : IPersonSearch
    {
        private readonly IElasticClient _client;

        public PersonSearch(IElasticClient client)
        {
            _client = client;
        }

        public T GetPerson<T>(int id) where T : class
        {
            QueryContainer query1 = new QueryDescriptor<Person>().Wildcard(p => p.Id, id.ToString());

            var request = new SearchRequest
            {
                From = 0,
                Size = 10,
                Query = query1
            };

            var result = _client.Search<T>(request);
            return result.Documents.FirstOrDefault();
        }
    }
}
