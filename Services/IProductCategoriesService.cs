using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Services
{
    public interface IProductCategoriesService
    {
        /// <summary>
        /// Get product categories
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync(Guid? organizationId);

        /// <summary>
        /// Get product category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductCategory> GetProductCategoryByIdAsync(Guid id);

        /// <summary>
        /// Create a new product category
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory);

        /// <summary>
        /// Update a product category
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        Task<ProductCategory> UpdateProductCategoryAsync(ProductCategory productCategory);

        /// <summary>
        /// Delete a product category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductCategory> DeleteProductCategoryAsync(Guid id, Guid organizationId);
    }
}
