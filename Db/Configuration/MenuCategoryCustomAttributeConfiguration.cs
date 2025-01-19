using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.Configuration
{
    internal sealed class MenuCategoryCustomAttributeConfiguration : IEntityTypeConfiguration<MenuCategoryCustomAttribute>
    {
        public void Configure(EntityTypeBuilder<MenuCategoryCustomAttribute> builder)
        {
            builder.ToTable(DbConstants.Table.MenuCategoryCustomAttribute, DbConstants.DefaultSchema);

            builder.HasKey(t => new { t.MenuCategoryId, t.CustomAttributeId });

            builder.Property(u => u.MenuCategoryId)
                .IsRequired()
                .HasColumnName("menucategory_id");
            builder.Property(u => u.CustomAttributeId)
                .IsRequired()
                .HasColumnName("customattribute_id");
        }
    }
}
