using System.Diagnostics.CodeAnalysis;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable(DbConstants.Table.Property, DbConstants.DefaultSchema);

            builder.HasKey(t => t.PropertyId);
            builder.HasIndex(t => new { t.OrganizationId, t.Name }).IsUnique();

            builder.Property(u => u.PropertyId)
                .HasColumnName("property_id");

            builder.Property(u => u.OrganizationId)
                .HasColumnName("organization_id")
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.IdName)
                .HasColumnName("idname")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(u => u.TypeName)
                .HasColumnName("typename")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(u => u.Enabled)
                .HasColumnName("enabled")
                .IsRequired();

            builder.HasOne(u => u.Organization)
                .WithMany(t => t.Properties)
                .HasForeignKey(u => u.OrganizationId);
        }
    }
}
