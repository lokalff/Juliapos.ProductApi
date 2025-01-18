using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Services
{
    public interface IProductsService
    {
        /// <summary>
        /// Get products
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetProductsAsync(Guid? organizationId);

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProductByIdAsync(Guid id);

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product> CreateProductAsync(Product product);

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product> UpdateProductAsync(Product product);

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="purge"></param>
        /// <returns></returns>
        Task<Product> DeleteProductAsync(Guid id, bool purge);
    }
}
