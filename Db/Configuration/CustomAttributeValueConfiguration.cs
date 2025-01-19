using System.Diagnostics.CodeAnalysis;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class CustomAttributeValueConfiguration : IEntityTypeConfiguration<CustomAttributeValue>
    {
        public void Configure(EntityTypeBuilder<CustomAttributeValue> builder)
        {
            builder.ToTable(DbConstants.Table.CustomAttributeValue, DbConstants.DefaultSchema);

            builder.HasKey(t => t.CustomAttributeValueId);
            builder.HasIndex(t => new { t.ProductId, t.CustomAttributeId });

            builder.Property(u => u.CustomAttributeValueId)
                .HasColumnName("customattributevalue_id");

            builder.Property(u => u.CustomAttributeId)
                .HasColumnName("customattribute_id")
                .IsRequired();

            builder.Property(u => u.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(u => u.Value)
                .HasColumnName("value")
                .IsRequired();

            builder.HasOne(pv => pv.CustomAttribute)
                .WithMany()
                .HasForeignKey(pv => pv.CustomAttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.CustomAttributeValues)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
