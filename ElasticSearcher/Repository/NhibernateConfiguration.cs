using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using ElasticSearcher.Models;

namespace ElasticSearcher.Repository
{

    

    public class BaseNhibernateConfiguration
    {

        public static Configuration NhibernateConfiguration;
        public static ISessionFactory SessionFactory;

        public static Configuration ConfigureNhibernate()
        {
            var configure = new Configuration();

            configure.SessionFactoryName("BuildId");

            configure.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
                db.ConnectionString = "Server=RICHARD-PC;Uid=sa;pwd=test;Database=NHibernateTest;";
                db.Timeout = 10;

                //testing

                db.LogFormattedSql = true;
                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;

            });

            return configure;

        }

        public static HbmMapping GetMappings ()
        {
            ModelMapper mapper = new ModelMapper();

            mapper.AddMapping<PersonMap>();
            mapper.AddMapping<PetMap>();

            HbmMapping mapping = mapper.CompileMappingFor(new[] { typeof(Person), typeof(Pet) });

            return mapping;
        }

    }

}
