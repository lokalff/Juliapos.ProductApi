using System.Diagnostics.CodeAnalysis;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(DbConstants.Table.Product, DbConstants.DefaultSchema);

            builder.HasKey(t => t.ProductId);

            builder.Property(u => u.ProductId)
                .HasColumnName("product_id");

            builder.Property(u => u.ProductCategoryId) // was ProductTypeId
                .HasColumnName("category_id")
                .IsRequired();

            builder.Property(u => u.DustCategoryId)
                .HasColumnName("dustcategory_id")
                .IsRequired();

            builder.Property(u => u.MenuCategoryId)
                .HasColumnName("menucategory_id")
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.MenuName)
                .HasColumnName("menuname")
                .HasMaxLength(50);

            builder.Property(u => u.Code)
                .HasColumnName("code")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(u => u.State)
                .HasColumnName("state");

            builder.Property(u => u.Description)
                .HasColumnName("description");

            builder.Property(u => u.VatLevel)
                .HasColumnName("vatlevel")
                .IsRequired();

            builder.Property(u => u.Percentage)
                .HasColumnName("percentage")
                .IsRequired();

            builder.Property(u => u.InInventory)
                .HasColumnName("ininventory")
                .IsRequired();

            builder.Property(u => u.AscendingStock)
                .HasColumnName("ascendingstock")
                .IsRequired();

            builder.Property(u => u.Created)
                .HasColumnName("created")
                .IsRequired();

            builder.Property(u => u.UserCreate)
                .HasColumnName("usercreate")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.UserUpdate)
                .HasColumnName("userupdate")
                .HasMaxLength(50);

            builder.Property(u => u.Updated)
                .HasColumnName("updated");

            builder.Property(u => u.UserDelete)
                .HasColumnName("userdelete")
                .HasMaxLength(50);

            builder.Property(u => u.Deleted)
                .HasColumnName("deleted");

            builder.HasOne(u => u.ProductCategory)
                .WithMany(t => t.Products)
                .HasForeignKey(u => u.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.DustCategory)
                .WithMany(t => t.Products)
                .HasForeignKey(u => u.DustCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.MenuCategory)
                .WithMany(t => t.Products)
                .HasForeignKey(u => u.MenuCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
