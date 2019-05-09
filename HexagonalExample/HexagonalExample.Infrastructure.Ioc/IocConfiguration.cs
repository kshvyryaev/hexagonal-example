using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HexagonalExample.Domain.Contracts.Adapters;
using HexagonalExample.Domain.Contracts.Services;
using HexagonalExample.Domain.Contracts.Repositories;
using HexagonalExample.Domain.Entities;

namespace HexagonalExample.Infrastructure.Ioc
{
    public static class IocConfiguration
    {
        private const string SelectedDatabaseKey = "SelectedDatabase";
        private const string EntityFrameworkSql = "EntityFrameworkSql";
        private const string Mongo = "Mongo";

        public static void ConfigureIoc(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.ConfigureServices();
            services.ConfigureValidators();
            services.ConfigureAdapters();

            string selectedDatabase = configuration[SelectedDatabaseKey];
            switch (selectedDatabase)
            {
                case Mongo:
                    services.ConfigureMongo(configuration);
                    break;
                case EntityFrameworkSql:
                default:
                    services.ConfigureEntityFrameworkSql(configuration);
                    break;
            }
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IBooksService, Domain.Core.BooksService>();
        }

        private static void ConfigureValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidatorAdapter<Book>, Validation.FluentValidation.BookValidatorAdapter>();
        }

        private static void ConfigureAdapters(this IServiceCollection services)
        {
            services.AddSingleton<ICacheAdapter, Caching.Memory.CacheAdapter>();
            services.AddSingleton<ILoggerAdapter, Logging.NLog.LoggerAdapter>();
            services.AddSingleton<IMapperAdapter, Mapping.AutoMapper.MapperAdapter>();
        }

        private static void ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            Data.Mongo.DatabaseConfiguration.ConfigureDatabase(configuration);
            services.AddScoped<IBooksRepository, Data.Mongo.BooksRepository>();
        }

        private static void ConfigureEntityFrameworkSql(this IServiceCollection services, IConfiguration configuration)
        {
            Data.EF.DatabaseContext.ConfigureContext(services, configuration);
            services.AddScoped<IBooksRepository, Data.EF.BooksRepository>();
        }
    }
}
