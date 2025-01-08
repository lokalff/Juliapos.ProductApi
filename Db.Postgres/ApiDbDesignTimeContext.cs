using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Npgsql;

namespace Juliapos.Portal.ProductApi.Db.Postgres
{
    [ExcludeFromCodeCoverage]
    public sealed class ApiDbDesignTimeContext : IDesignTimeDbContextFactory<ApiDbContext>
    {
        /// <summary>
        /// Creates an instance of <see cref="ApiDbContext"/> specifically for design time work.
        /// </summary>
        /// <param name="args">Input arguments (not used)</param>
        /// <returns>An instance of <see cref="ApiDbContext" />.</returns>
        public ApiDbContext CreateDbContext(string[] args)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Database = "juliaposdb",
                Username = "postgres",
                Password = "kahaL01m@"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
            optionsBuilder.UseNpgsql(builder.ConnectionString, DefaultOptionsBuilder);

            return new ApiDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Constructs the default options for our purposes.
        /// </summary>
        /// <param name="optionsBuilder">The builder to configure the DB options with.</param>
        public static void DefaultOptionsBuilder(NpgsqlDbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.MigrationsAssembly(typeof(ApiDbDesignTimeContext).Assembly.FullName);
            optionsBuilder.MigrationsHistoryTable(DbConstants.HistoryTableName, DbConstants.DefaultSchema);
            optionsBuilder.EnableRetryOnFailure();
        }

    }
}
