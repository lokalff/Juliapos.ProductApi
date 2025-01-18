using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="ProductCategoryQuery"/>
    /// </summary>
    public sealed class ProductCategoryQueryHandler : IHandleQuery<ProductCategoryQuery, ProductCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="ProductCategoryQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public ProductCategoryQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> HandleAsync(ProductCategoryQuery query)
        {
            var result = await m_dataStore.ProductCategoryDataQuery
                .WithOrganization()
                .WhereOrganizationId(query.OrganizationId)
                .WhereId(query.Id)
                .SingleOrDefaultAsync()
                ?? throw new HttpNotFoundException(ApiErrorCode.DustCategoryNotFound, query.Id); ;

            return result;
        }
    }
}
