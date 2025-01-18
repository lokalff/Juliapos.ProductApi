using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
    {
        public void Configure(EntityTypeBuilder<MenuCategory> builder)
        {
            builder.ToTable(DbConstants.Table.MenuCategory, DbConstants.DefaultSchema);

            builder.HasKey(t => t.MenuCategoryId);
            builder.HasIndex(t => new { t.OrganizationId, t.Name }).IsUnique();
            builder.HasIndex(t => new { t.OrganizationId, t.IdName }).IsUnique();

            builder.Property(u => u.MenuCategoryId)
                .HasColumnName("menucategory_id");

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

            builder.Property(u => u.Enabled)
                .HasColumnName("enabled")
                .IsRequired();

            builder.Property(u => u.Weight)
                .HasColumnName("weight")
                .IsRequired();

            builder.HasMany(c => c.Properties)
                .WithMany(p => p.MenuCategories)
                .UsingEntity<MenuCategoryProperty>();

            builder.HasOne(u => u.Organization)
                .WithMany(t => t.MenuCategories)
                .HasForeignKey(u => u.OrganizationId);


        }
    }
}
