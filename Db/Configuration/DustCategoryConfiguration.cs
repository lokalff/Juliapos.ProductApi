using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class DustCategoryConfiguration : IEntityTypeConfiguration<DustCategory>
    {
        public void Configure(EntityTypeBuilder<DustCategory> builder)
        {
            builder.ToTable(DbConstants.Table.DustCategory, DbConstants.DefaultSchema);

            builder.HasKey(t => t.DustCategoryId);
            builder.HasIndex(t => new { t.OrganizationId, t.Name }).IsUnique();

            builder.Property(u => u.DustCategoryId)
                .HasColumnName("dustcategory_id");

            builder.Property(u => u.OrganizationId)
                .HasColumnName("organization_id")
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Weight)
                .HasColumnName("weight")
                .IsRequired();

        }
    }
}
