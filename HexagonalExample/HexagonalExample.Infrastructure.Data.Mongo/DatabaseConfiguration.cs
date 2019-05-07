using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace HexagonalExample.Infrastructure.Data.Mongo
{
    public static class DatabaseConfiguration
    {
        private const string MongoConnectionString = "Mongo:ConnectionString";
        private const string MongoDatabase = "Mongo:Database";

        internal static IMongoDatabase Database { get; private set; }

        public static void ConfigureDatabase(this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var client = new MongoClient(configuration[MongoConnectionString]);
            Database = client.GetDatabase(configuration[MongoDatabase]);
        }
    }
}
