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

            // Services
            services.AddSingleton<IBooksService, Domain.Core.BooksService>();

            // Repositories
            Data.Mongo.DatabaseConfiguration.ConfigureDatabase(configuration);
            services.AddSingleton<IBooksRepository, Data.Mongo.BooksRepository>();

            // Validators
            services.AddTransient<IValidatorAdapter<Book>, Validation.FluentValidation.BookValidatorAdapter>();

            // Adapters
            services.AddSingleton<ICacheAdapter, Caching.Memory.CacheAdapter>();
            services.AddSingleton<ILoggerAdapter, Logging.NLog.LoggerAdapter>();
            services.AddSingleton<IMapperAdapter, Mapping.AutoMapper.MapperAdapter>();
        }
    }
}
