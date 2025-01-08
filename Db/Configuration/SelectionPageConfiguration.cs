using System.Diagnostics.CodeAnalysis;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class SelectionPageConfiguration : IEntityTypeConfiguration<SelectionPage>
    {
        public void Configure(EntityTypeBuilder<SelectionPage> builder)
        {
            builder.ToTable(DbConstants.Table.SelectionPage, DbConstants.DefaultSchema);

            builder.HasKey(t => t.SelectionPageId);
            builder.HasIndex(t => new { t.OrganizationId, t.Name }).IsUnique();

            builder.Property(u => u.SelectionPageId)
                .HasColumnName("selectionpage_id");

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

        }
    }
}
