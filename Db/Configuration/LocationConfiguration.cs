using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Juliapos.Portal.ProductApi.Db.Models;
using System.Diagnostics.CodeAnalysis;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable(DbConstants.Table.Location, DbConstants.DefaultSchema);

            builder.HasKey(t => t.LocationId);
            builder.HasIndex(t => new { t.OrganizationId, t.Name }).IsUnique();

            builder.Property(u => u.LocationId)
                .HasColumnName("location_id");

            builder.Property(u => u.OrganizationId)
                .HasColumnName("organization_id")
                .IsRequired();

            builder.Property(u => u.ExternalId)
                .HasColumnName("external_id")
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(o => o.Organization)
                .WithMany(l => l.Locations)
                .HasForeignKey(o => o.OrganizationId);

        }
    }
}
