using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="ProductCategoryUpdateCommand"/>
    /// </summary>
    public sealed class ProductCategoryUpdateCommandHandler : IHandleCommand<ProductCategoryUpdateCommand, ProductCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="ProductCategoryUpdateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public ProductCategoryUpdateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> HandleAsync(ProductCategoryUpdateCommand command)
        {
            var existingCategory = await m_dataStore.ProductCategoryDataQuery
                .WithOrganization()
                .WhereId(command.Id)
                .SingleOrDefaultAsync()
                ?? throw new HttpNotFoundException(ApiErrorCode.DustCategoryNotFound, command.Id);

            if (!command.Enabled && existingCategory.Enabled)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .WhereProductCategoryId(existingCategory.ProductCategoryId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.ProductCategoryHasProducts, existingCategory.ProductCategoryId);
            }

            existingCategory.Name = command.Name;
            existingCategory.MeasureMethod = command.MeasureMethod;
            existingCategory.DefaultForeColor = command.DefaultForeColor;
            existingCategory.DefaultBackColor = command.DefaultBackColor;
            existingCategory.Weight = command.Weight;
            existingCategory.Enabled = command.Enabled;
            await m_dataStore.SaveChangesAsync();

            return existingCategory;
        }
    }
}
