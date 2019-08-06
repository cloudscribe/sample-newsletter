using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudscribeFeatures
    {
        public static IServiceCollection SetupDataStorage(
            this IServiceCollection services,
            IConfiguration config
            )
        {
            var dbPlatform = config["DataSettings:DbPlatform"];
            switch (dbPlatform)
            {
                case "pgsql":
                    var pgSqlConnectionString = config["DataSettings:PostgreSqlConnectionString"];
                    
                    services.AddCloudscribeLoggingPostgreSqlStorage(pgSqlConnectionString);
                    services.AddCloudscribeCorePostgreSqlStorage(pgSqlConnectionString);
                    services.AddCloudscribeSimpleContentPostgreSqlStorage(pgSqlConnectionString);
                    services.AddCloudscribeKvpPostgreSqlStorage(pgSqlConnectionString);
                    services.AddEmailTemplatePostgreSqlStorage(pgSqlConnectionString);
                    services.AddEmailQueuePostgreSqlStorage(pgSqlConnectionString);
                    services.AddEmailListPostgreSqlStorage(pgSqlConnectionString);


                    break;

                case "mysql":
                    var mySqlConnectionString = config["DataSettings:MySqlConnectionString"];
                    
                    services.AddCloudscribeLoggingEFStorageMySQL(mySqlConnectionString);
                    services.AddCloudscribeCoreEFStorageMySql(mySqlConnectionString);
                    services.AddCloudscribeSimpleContentEFStorageMySQL(mySqlConnectionString);
                    services.AddCloudscribeKvpEFStorageMySql(mySqlConnectionString);
                    services.AddEmailTemplateStorageMySql(mySqlConnectionString);
                    services.AddEmailQueueStorageMySql(mySqlConnectionString);
                    services.AddEmailListStorageMySql(mySqlConnectionString);

                    break;

                case "mssql":
                default:

                    var msSqlConnectionString = config["DataSettings:MsSqlConnectionString"];
                    
                    services.AddCloudscribeLoggingEFStorageMSSQL(msSqlConnectionString);
                    services.AddCloudscribeCoreEFStorageMSSQL(msSqlConnectionString);
                    services.AddCloudscribeSimpleContentEFStorageMSSQL(msSqlConnectionString);
                    services.AddCloudscribeKvpEFStorageMSSQL(msSqlConnectionString);
                    services.AddEmailTemplateStorageMSSQL(msSqlConnectionString);
                    services.AddEmailQueueStorageMSSQL(msSqlConnectionString);
                    services.AddEmailListStorageMSSQL(msSqlConnectionString);
                    
                    break;

            }
            
            return services;
        }

        public static IServiceCollection SetupCloudscribeFeatures(
            this IServiceCollection services,
            IConfiguration config
            )
        {

            services.AddCloudscribeLogging(config);


            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.Web.Navigation.NavigationNodePermissionResolver>();
            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.SimpleContent.Web.Services.PagesNavigationNodePermissionResolver>();
            services.AddCloudscribeCoreMvc(config);
            services.AddCloudscribeCoreIntegrationForSimpleContent(config);
            services.AddSimpleContentMvc(config);
            services.AddContentTemplatesForSimpleContent(config);

            services.AddMetaWeblogForSimpleContent(config.GetSection("MetaWeblogApiOptions"));
            services.AddSimpleContentRssSyndiction();

            services.AddEmailQueueBackgroundTask(config);

            services.Configure<cloudscribe.UserProperties.Models.ProfilePropertySetContainer>(config.GetSection("ProfilePropertySetContainer"));
            services.AddEmailListKvpIntegration(config);
            services.AddCloudscribeKvpUserProperties();
            services.AddEmailQueueWithCloudscribeIntegration(config);
            services.AddEmailListWithCloudscribeIntegration(config);


            return services;
        }

    }
}
