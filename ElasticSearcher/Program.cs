using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticSearcher.Models;
using ElasticSearcher.Index;
using ElasticSearcher.Repository;
using Nest;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using ElasticSearcher.Models;

namespace ElasticSearcher
{
    class Program
    {

        protected static IList<Pet> Pets;
        protected static IList<Person> People;

        static void Main(string[] args)
        {
            var node = new Uri("http://localhost:9200");

            var settings = new ConnectionSettings(
                node,
                defaultIndex: "my-application"
            );

            var client = new ElasticClient(settings);

            //PersonIndexer.IndexPerson(client);

            Setup();
            CreateDatabaseSchema();
            InsertData();
            ReadData();
        }

        public static void Setup ()
        {
            BaseNhibernateConfiguration.NhibernateConfiguration = BaseNhibernateConfiguration.ConfigureNhibernate();

            HbmMapping mapping = BaseNhibernateConfiguration.GetMappings();

            BaseNhibernateConfiguration.NhibernateConfiguration.AddDeserializedMapping(mapping, "NHSchemeTest");

            SchemaMetadataUpdater.QuoteTableAndColumns(BaseNhibernateConfiguration.NhibernateConfiguration);

            BaseNhibernateConfiguration.SessionFactory = BaseNhibernateConfiguration.NhibernateConfiguration.BuildSessionFactory();
        }

        public static void CreateDatabaseSchema ()
        {
            //new SchemaExport(BaseNhibernateConfiguration.NhibernateConfiguration).Drop(false, true);
            new SchemaExport(BaseNhibernateConfiguration.NhibernateConfiguration).Create(false, true);
        }

        public static bool ValidateSchema ()
        {
            try
            {
                SchemaValidator schemaValidator = new SchemaValidator(BaseNhibernateConfiguration.NhibernateConfiguration);
                schemaValidator.Validate();
                return true;
            } catch (HibernateException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

        public static void InsertData ()
        {
            ISession session = BaseNhibernateConfiguration.SessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            Person person1 = new Person
            {
                Name = "Richard",
                Id = 1,
                Job = "Developer",
            };

            Pet pet1 = new Pet
            {
                Name = "Stink",
                Id = 1,
                Age = 1
            };

            person1.pet.Add(pet1);
            session.SaveOrUpdate(person1); ;
            session.SaveOrUpdate(pet1);
            transaction.Commit();

        }

        public static void ReadData ()
        {
            if (ValidateSchema())
            {
                ISession session = BaseNhibernateConfiguration.SessionFactory.OpenSession();
                ITransaction transaction = session.BeginTransaction();

                Person person = session.Get<Person>(1);

                if(person != null)
                {
                    Console.WriteLine(person.Name);
                } else
                {
                    Console.WriteLine("Cant find...");
                }
            } else
            {
                Console.WriteLine("Validation failed..");
                Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}
