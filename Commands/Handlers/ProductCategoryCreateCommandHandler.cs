using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="ProductCategoryCreateCommand"/>
    /// </summary>
    public sealed class ProductCategoryCreateCommandHandler : IHandleCommand<ProductCategoryCreateCommand, ProductCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="ProductCategoryCreateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public ProductCategoryCreateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> HandleAsync(ProductCategoryCreateCommand command)
        {
            var category = new ProductCategory
            {
                OrganizationId = command.OrganizationId,
                Name = command.Name,
                IdName = command.IdName,
                MeasureMethod = command.MeasureMethod,
                DefaultForeColor = command.DefaultForeColor,
                DefaultBackColor = command.DefaultBackColor,
                Enabled = command.Enabled,
                Weight = command.Weight,
            };
            
            m_dataStore.Add(category);
            await m_dataStore.SaveChangesAsync();
           
            return category;
        }
    }
}
