using System.Diagnostics.CodeAnalysis;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    [ExcludeFromCodeCoverage]
    internal sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable(DbConstants.Table.Organization, DbConstants.DefaultSchema);

            builder.HasKey(t => t.OrganizationId);

            builder.Property(u => u.OrganizationId)
                .HasColumnName("organization_id");

            builder.Property(u => u.ExternalId)
                .HasColumnName("external_id");

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            //builder.HasData(
            //    new Organization
            //    {
            //        OrganizationId = DbConstants.Ids.Organization.Demo,
            //        Name = "Demo organization"
            //    }
            //);

        }
    }
}
