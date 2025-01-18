using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="ProductCategoryDeleteCommand"/>
    /// </summary>
    public sealed class ProductCategoryDeleteCommandHandler : IHandleCommand<ProductCategoryDeleteCommand, ProductCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="ProductCategoryDeleteCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public ProductCategoryDeleteCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> HandleAsync(ProductCategoryDeleteCommand command)
        {
            var result = await m_dataStore.ProductCategoryDataQuery
                .WhereId(command.Id)
                .WhereOrganizationId(command.OrganizationId)
                .SingleOrDefaultAsync();

            if (result != null)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .WhereProductCategoryId(result.ProductCategoryId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.ProductCategoryHasProducts, result.ProductCategoryId);

                m_dataStore.Remove(result);
                await m_dataStore.SaveChangesAsync();
            }
            return result;
        }
    }
}
