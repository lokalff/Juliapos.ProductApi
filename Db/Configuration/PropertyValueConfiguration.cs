using System.Diagnostics.CodeAnalysis;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class PropertyValueConfiguration : IEntityTypeConfiguration<PropertyValue>
    {
        public void Configure(EntityTypeBuilder<PropertyValue> builder)
        {
            builder.ToTable(DbConstants.Table.PropertyValue, DbConstants.DefaultSchema);

            builder.HasKey(t => t.PropertyValueId);

            builder.Property(u => u.PropertyValueId)
                .HasColumnName("propertyvalue_id");

            builder.Property(u => u.PropertyId)
                .HasColumnName("property_id")
                .IsRequired();

            builder.Property(u => u.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(u => u.Value)
                .HasColumnName("value")
                .IsRequired();

            builder.HasOne(pv => pv.Property)
                .WithMany()
                .HasForeignKey(pv => pv.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.PropertieValues)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
