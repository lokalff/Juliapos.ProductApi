using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class SelectionPageProductConfiguration : IEntityTypeConfiguration<SelectionPageProduct>
    {
        public void Configure(EntityTypeBuilder<SelectionPageProduct> builder)
        {
            builder.ToTable(DbConstants.Table.SelectionPageProduct, DbConstants.DefaultSchema);

            builder.HasKey(t => t.SelectionPageProductId);

            builder.Property(u => u.SelectionPageProductId)
                .HasColumnName("selectionpageproduct_id");

            builder.Property(u => u.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(u => u.SelectionPageId)
                .HasColumnName("selectionpage_id")
                .IsRequired();

            builder.Property(u => u.RowIdx)
                .HasColumnName("row")
                .IsRequired();

            builder.Property(u => u.ColumnIdx)
                .HasColumnName("column")
                .IsRequired();

            builder.Property(u => u.ForeColor)
                .HasColumnName("forecolor")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(u => u.BackColor)
                .HasColumnName("backcolor")
                .IsRequired()
                .HasMaxLength(32);

            builder.HasOne(v => v.Product)
                .WithMany(p => p.SelectionPageProducts)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.SelectionPage)
                .WithMany(p => p.SelectionPageProducts)
                .HasForeignKey(v => v.SelectionPageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
