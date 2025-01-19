using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db
{
    public interface IApiDbContext
    {
        DbSet<DustCategory> DustCategory { get; }
        DbSet<MenuCategory> MenuCategory { get; }
        DbSet<ProductCategory> ProductCategory { get; }
        DbSet<Location> Location { get; }
        DbSet<Organization> Organization { get; }
        DbSet<Product> Product { get; }
        DbSet<ProductVariation> ProductVariation { get; }
        DbSet<ProductVariationLocation> ProductVariationLocation { get; }
        DbSet<CustomAttribute> CustomAttribute { get; }
        DbSet<CustomAttributeValue> CustomAttributeValue { get; }
        DbSet<SelectionPage> SelectionPage { get; }
        DbSet<SelectionPageProduct> SelectionPageProduct { get; }
        DbSet<MenuCategoryCustomAttribute> MenuCategoryCustomAttribute { get; }
    }
}
