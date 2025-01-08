using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Services.Implementation
{
    public sealed class ProductsService : IProductsService
    {

        /// <inheritdoc />
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return null;
        }
    }
}
