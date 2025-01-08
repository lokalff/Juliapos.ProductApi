using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db
{
    public interface IApiDbContext
    {
        DbSet<DustCategory> DustCategory { get; }
        DbSet<MenuCategory> MenuCategory { get; }
        DbSet<Location> Location { get; }
        DbSet<Organization> Organization { get; }
        DbSet<Product> Product { get; }
        DbSet<ProductCategory> ProductType { get; }
        DbSet<ProductVariation> ProductVariation { get; }
        DbSet<ProductVariationLocation> ProductVariationLocation { get; }
        DbSet<Property> Property { get; }
        DbSet<PropertyValue> PropertyValue { get; }
        DbSet<SelectionPage> SelectionPage { get; }
        DbSet<SelectionPageProduct> SelectionPageProduct { get; }
        DbSet<MenuCategoryProperty> ProductPropertyTemplate { get; }
    }
}
