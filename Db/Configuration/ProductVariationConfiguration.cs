using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Juliapos.Portal.ProductApi.Db.Models;
using System.Diagnostics.CodeAnalysis;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class ProductVariationConfiguration : IEntityTypeConfiguration<ProductVariation>
    {
        public void Configure(EntityTypeBuilder<ProductVariation> builder)
        {
            builder.ToTable(DbConstants.Table.ProductVariation, DbConstants.DefaultSchema);

            builder.HasKey(t => t.ProductVariationId);

            // These indexes do not work (globally) productId is unique in itself
            // It prevents multiple entries within the product
            builder.HasIndex(t => new { t.ProductId, t.Sku });
            builder.HasIndex(t => new { t.ProductId, t.CodeExtension });
            builder.HasIndex(t => new { t.ProductId, t.NameExtension });

            builder.Property(u => u.ProductVariationId)
                .HasColumnName("productvariation_id");

            builder.Property(u => u.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Property(u => u.CodeExtension)
                .HasColumnName("codeextension")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(u => u.NameExtension)
                .HasColumnName("nameextension")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Sku)
                .HasColumnName("sku")
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(v => v.Product)
                .WithMany(p => p.ProductVariations)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
