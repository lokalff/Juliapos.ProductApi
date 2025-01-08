using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    internal sealed class MenuCategoryPropertyConfiguration : IEntityTypeConfiguration<MenuCategoryProperty>
    {
        public void Configure(EntityTypeBuilder<MenuCategoryProperty> builder)
        {
            builder.ToTable(DbConstants.Table.MenuCategoryProperty, DbConstants.DefaultSchema);

            builder.HasKey(t => new { t.MenuCategoryId, t.PropertyId });

            builder.Property(u => u.MenuCategoryId)
                .IsRequired()
                .HasColumnName("menucategory_id");
            builder.Property(u => u.PropertyId)
                .IsRequired()
                .HasColumnName("property_id");
        }
    }
}
