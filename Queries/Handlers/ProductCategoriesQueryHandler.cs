using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="ProductCategoriesQuery"/>
    /// </summary>
    public sealed class ProductCategoriesQueryHandler : IHandleQuery<ProductCategoriesQuery, IEnumerable<ProductCategory>>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="ProductCategoriesQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public ProductCategoriesQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProductCategory>> HandleAsync(ProductCategoriesQuery query)
        {
            var q = m_dataStore.ProductCategoryDataQuery
                .WithOrganization();

            if (query.OrganizationId != null)
                q = q.WhereOrganizationId(query.OrganizationId.Value);

            var result = await q.ToListAsync();

            return result;
        }
    }
}
