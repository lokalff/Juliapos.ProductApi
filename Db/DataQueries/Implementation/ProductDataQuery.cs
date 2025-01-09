using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class ProductDataQuery : DataQuery<IApiDbContext, Product>, IProductDataQuery
    {
        public ProductDataQuery(IApiDbContext dataContext)
            : this(dataContext, dataContext.Product
                  .Include(p => p.ProductCategory)
                  .ThenInclude(c => c.Organization))
        {
        }

        private ProductDataQuery(IApiDbContext dataContext, IQueryable<Product> queryable)
            : base(dataContext, queryable)
        {
        }

        public IProductDataQuery WithVariations()
        {
            return new ProductDataQuery(DataContext, Queryable
                .Include(c => c.ProductVariations)
                .ThenInclude(v => v.ProductVariationLocations));
        }

        public IProductDataQuery WithLocations()
        {
            return new ProductDataQuery(DataContext, Queryable
                .Include(c => c.ProductVariations)
                .ThenInclude(v => v.ProductVariationLocations)
                .ThenInclude(lv => lv.Location));
        }

        public IProductDataQuery WithProperties()
        {
            return new ProductDataQuery(DataContext, Queryable
                .Include(p => p.PropertieValues)
                .ThenInclude(p => p.Property));
        }

        public IProductDataQuery WhereId(Guid id)
        {
            return new ProductDataQuery(DataContext, Queryable.Where(c => c.ProductId == id));
        }

        public IProductDataQuery WhereOrganizationId(Guid id)
        {
            return new ProductDataQuery(DataContext, Queryable.Where(c => c.ProductCategory.OrganizationId == id));
        }

        public IProductDataQuery WhereOrganizationExternalId(Guid id)
        {
            return new ProductDataQuery(DataContext, Queryable.Where(c => c.ProductCategory.Organization.ExternalId == id));
        }
    }
}
