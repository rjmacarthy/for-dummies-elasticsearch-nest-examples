using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElasticSearcher.Models
{
    public class Pet
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual Person person {get; set;}
    }

    public class PetMap : ClassMapping<Pet>
    {
        public PetMap()
        {
            Id<int>(x => x.Id);
            Property<string>(x => x.Name);
            Property<int>(x => x.Age);
            ManyToOne<Person>(x => x.person);
        }
    }
}
