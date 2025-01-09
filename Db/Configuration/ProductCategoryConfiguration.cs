using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable(DbConstants.Table.ProductCategory, DbConstants.DefaultSchema);

            builder.HasKey(t => t.ProductCategoryId);
            builder.HasIndex(t => new { t.OrganizationId, t.Name }).IsUnique();

            builder.Property(u => u.ProductCategoryId)
                .HasColumnName("productcategory_id");

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

            builder.Property(u => u.MeasureMethod)
                .HasColumnName("measuremethod")
                .IsRequired();

            builder.Property(u => u.DefaultForeColor)
                .HasColumnName("forecolor")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(u => u.DefaultBackColor)
                .HasColumnName("backcolor")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(u => u.Enabled)
                .HasColumnName("enabled")
                .IsRequired();

            builder.Property(u => u.Weight)
                .HasColumnName("weight")
                .IsRequired();

            builder.HasOne(u => u.Organization)
                .WithMany(t => t.ProductCategories)
                .HasForeignKey(u => u.OrganizationId);

        }
    }
}
