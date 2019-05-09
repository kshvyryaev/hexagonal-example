using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HexagonalExample.Infrastructure.Data.EF.Models;
using HexagonalExample.Infrastructure.Data.EF.Configurations;

namespace HexagonalExample.Infrastructure.Data.EF
{
    public class DatabaseContext : DbContext
    {
        #region Constants

        private const string ConnectionStringName = "DefaultConnection";

        #endregion Constants

        #region Constructors

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion Constructors

        #region Properties

        public DbSet<BookEFModel> Books { get; set; }

        public DbSet<AuthorEFModel> Authors { get; set; }

        #endregion Properties

        #region Methods

        public static void ConfigureContext(IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            string connectionString = configuration.GetConnectionString(ConnectionStringName);
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        }

        #endregion Methods
    }
}
