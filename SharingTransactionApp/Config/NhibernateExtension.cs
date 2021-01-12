using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using SharingTransactionApp.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Config
{
    public static class NhibernateExtension
    {
        public static IServiceCollection AddNhibernate(this IServiceCollection services,string connectionString,bool updateSchema )
        {
            var cfg = Fluently.Configure().Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BalanceMapping>()).BuildConfiguration();
            var sessionFactory = Fluently.Configure(cfg).BuildSessionFactory();

            if(updateSchema)
            {
                var update = new SchemaUpdate(cfg);
                update.Execute(false, true);
            }

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }
    }
}
