using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Db.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Juliapos.Portal.ProductApi.Db
{
    /// <summary>
    /// Api Db context
    /// </summary>
    public sealed class ApiDbContext : DbContext, IApiDbContext
    {
        //public DbSet<Organization> Organization { get; set; }
        public DbSet<DustCategory> DustCategory { get; set; }
        public DbSet<MenuCategory> MenuCategory { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<SelectionPage> SelectionPage { get; set; }
        public DbSet<SelectionPageProduct> SelectionPageProduct { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyValue> PropertyValue { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductVariation> ProductVariation { get; set; }
        public DbSet<ProductVariationLocation> ProductVariationLocation { get; set; }
        public DbSet<MenuCategoryProperty> ProductPropertyTemplate { get; set; }

        /// <summary>
        /// Create an instance of <see href="ApiDbContext" />
        /// </summary>
        /// <param name="options"></param>
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DustCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new MenuCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SelectionPageConfiguration());
            modelBuilder.ApplyConfiguration(new SelectionPageProductConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyValueConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariationConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariationLocationConfiguration());
            modelBuilder.ApplyConfiguration(new MenuCategoryPropertyConfiguration());

            //modelBuilder
            //    .ApplyConfiguration(new GetPackagesAtConfiguration())
            //    .HasDbFunction(typeof(ApiDbContext)
            //    .GetMethod(nameof(GetPackagesAt), new[] { typeof(DateTime), typeof(Guid?), typeof(Guid?) }))
            //    .HasSchema(DbConstants.DefaultSchema)
            //    .HasName(DbConstants.Function.GetPackagesAt);

            //modelBuilder
            //    .ApplyConfiguration(new GetPackagesEnclosingConfiguration())
            //    .HasDbFunction(typeof(ApiDbContext)
            //    .GetMethod(nameof(GetPackagesEnclosing), new[] { typeof(Guid) }))
            //    .HasSchema(DbConstants.DefaultSchema)
            //    .HasName(DbConstants.Function.GetPackagesEnclosing);

        }

        public void EnsureDbCreated()
        {
            // Look out, we can not actually use the EnsureDbCreated method on
            // the database property of the client. Per the documentation on this
            // method, a database created with EnsureDbCreated can not be updated
            // using migrations. Contrary to its name, the Migrate method actually
            // also creates the database if it does not exist.
            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Database currently contains the following migrations:");
            Database.GetAppliedMigrations().ToList().ForEach(s => Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {s}"));

            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] The following migrations are pending and will be performed in order:");
            Database.GetPendingMigrations().ToList().ForEach(s => Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {s}"));

            Database.Migrate();
        }


        public static void EnsureDbCreated(string connectionString, string provider, bool debugLogging)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
            switch (provider.ToLower())
            {
                //case "sqlserver":
                //    optionsBuilder.UseSqlServer(connectionString, optionsBuilder =>
                //    {
                //        optionsBuilder.MigrationsAssembly(DbConstants.MigrationsAssemblyPrefix + provider);
                //        optionsBuilder.MigrationsHistoryTable(DbConstants.HistoryTableName, DbConstants.DefaultSchema);
                //        optionsBuilder.EnableRetryOnFailure();
                //    });
                //    break;

                case "postgres":
                    optionsBuilder.UseNpgsql(connectionString, optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(DbConstants.MigrationsAssemblyPrefix + provider);
                        optionsBuilder.MigrationsHistoryTable(DbConstants.HistoryTableName, DbConstants.DefaultSchema);
                        optionsBuilder.EnableRetryOnFailure();
                    });
                    break;
            }

            if (debugLogging)
            {
                optionsBuilder.LogTo(
                    (log) =>
                    {
                        Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {log}");
                    }, LogLevel.Debug)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }

            using var client = new ApiDbContext(optionsBuilder.Options);
            // Look out, we can not actually use the EnsureDbCreated method on
            // the database property of the client. Per the documentation on this
            // method, a database created with EnsureDbCreated can not be updated
            // using migrations. Contrary to its name, the Migrate method actually
            // also creates the database if it does not exist.
            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Database currently contains the following migrations:");
            client.Database.GetAppliedMigrations().ToList().ForEach(s => Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {s}"));

            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] The following migrations are pending and will be performed in order:");
            client.Database.GetPendingMigrations().ToList().ForEach(s => Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {s}"));

            client.Database.Migrate();
        }

        //public IQueryable<PackagesAtResult> GetPackagesAt(DateTime at, Guid? OrganizationId, Guid? locationId)
        //    => FromExpression(() => GetPackagesAt(at, OrganizationId, locationId));

        //public IQueryable<PackageReference> GetPackagesEnclosing(Guid packageId)
        //    => FromExpression(() => GetPackagesEnclosing(packageId));

    }
}
