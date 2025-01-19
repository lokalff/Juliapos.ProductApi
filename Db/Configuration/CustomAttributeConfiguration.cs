using System.Diagnostics.CodeAnalysis;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class CustomAttributeConfiguration : IEntityTypeConfiguration<CustomAttribute>
    {
        public void Configure(EntityTypeBuilder<CustomAttribute> builder)
        {
            builder.ToTable(DbConstants.Table.CustomAttribute, DbConstants.DefaultSchema);

            builder.HasKey(t => t.CustomAttributeId);
            builder.HasIndex(t => new { t.OrganizationId, t.Name }).IsUnique();
            builder.HasIndex(t => new { t.OrganizationId, t.IdName }).IsUnique();

            builder.Property(u => u.CustomAttributeId)
                .HasColumnName("customattribute_id");

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

            builder.Property(u => u.TypeName)
                .HasColumnName("typename")
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(u => u.Enabled)
                .HasColumnName("enabled")
                .IsRequired();

            builder.HasOne(u => u.Organization)
                .WithMany(t => t.CustomAttributes)
                .HasForeignKey(u => u.OrganizationId);
        }
    }
}
