using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Services
{
    public interface IProductsService
    {
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProductByIdAsync(Guid id);
    }
}
