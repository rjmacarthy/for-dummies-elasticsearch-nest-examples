using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElasticSearcher.Models
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Job { get; set; }
        public virtual IList<Pet> pet { get; set; } 

        public Person ()
        {
            pet = new List<Pet>();
        }

    }


    public class PersonMap : ClassMapping<Person>
    {
        public PersonMap()
        {
            Id<int>(x => x.Id);
            Property<string>(x => x.Name);
            Property<string>(x => x.Job);
            List<Pet>(x => x.pet, cp => { }, cr => cr.OneToMany(x => x.Class(typeof(Pet))));
        }
    }
}
