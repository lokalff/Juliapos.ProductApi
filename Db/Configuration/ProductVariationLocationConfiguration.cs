using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Juliapos.Portal.ProductApi.Db.Models;
using System.Diagnostics.CodeAnalysis;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class ProductVariationLocationConfiguration : IEntityTypeConfiguration<ProductVariationLocation>
    {
        public void Configure(EntityTypeBuilder<ProductVariationLocation> builder)
        {
            builder.ToTable(DbConstants.Table.ProductVariationLocation, DbConstants.DefaultSchema);

            builder.HasKey(t => t.ProductVariationLocationId);

            builder.Property(u => u.ProductVariationLocationId)
                .HasColumnName("productvariationlocation_id");

            builder.Property(u => u.ProductVariationId)
                .IsRequired()
                .HasColumnName("productvariation_id");

            builder.Property(u => u.LocationId)
                .IsRequired()
                .HasColumnName("location_id");

            builder.Property(u => u.UnitPrice)
                .HasColumnName("unitprice");

            builder.Property(u => u.UnitPricePurchase)
                .HasColumnName("unitpricepurchase");

            builder.Property(u => u.MinAmount)
                .HasColumnName("minamount");

            builder.Property(u => u.MaxAmount)
                .HasColumnName("maxamount");

            builder.Property(u => u.ShowOnFavoritePage)
                .HasColumnName("favorite");

            builder.Property(u => u.Transport)
                .HasColumnName("transport");

            builder.Property(u => u.Status)
                .HasColumnName("status");

            builder.Property(u => u.NextStatus)
                .HasColumnName("nextstatus");

            builder.Property(u => u.ChangeDateTime)
                .HasColumnName("changedatetime");

            builder.Property(u => u.OnMenuStart)
                .HasColumnName("onmenustart");

            builder.Property(u => u.OnMenuEnd)
                .HasColumnName("onmenuend");


            builder.HasOne(v => v.ProductVariation)
                .WithMany(p => p.ProductVariationLocations)
                .HasForeignKey(v => v.ProductVariationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Location)
                .WithMany(p => p.ProductVariationLocations)
                .HasForeignKey(v => v.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
