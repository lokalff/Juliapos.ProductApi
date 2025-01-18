using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class ProductCategoryDataQuery : DataQuery<IApiDbContext, ProductCategory>, IProductCategoryDataQuery
    {
        public ProductCategoryDataQuery(IApiDbContext dataContext)
            : this(dataContext, dataContext.ProductCategory)
        {
        }

        private ProductCategoryDataQuery(IApiDbContext dataContext, IQueryable<ProductCategory> queryable)
            : base(dataContext, queryable)
        {
        }

        public IProductCategoryDataQuery WithOrganization()
        {
            return new ProductCategoryDataQuery(DataContext, Queryable.Include(c => c.Organization));
        }

        public IProductCategoryDataQuery WhereId(Guid id)
        {
            return new ProductCategoryDataQuery(DataContext, Queryable.Where(c => c.ProductCategoryId == id));
        }

        public IProductCategoryDataQuery WhereOrganizationId(Guid id)
        {
            return new ProductCategoryDataQuery(DataContext, Queryable.Where(c => c.OrganizationId == id));
        }

        public IProductCategoryDataQuery WhereOrganizationExternalId(Guid id)
        {
            return new ProductCategoryDataQuery(DataContext, Queryable
                .Include(c => c.Organization)
                .Where(c => c.Organization.ExternalId == id));
        }

    }
}
