using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Services
{
    public interface IEndpointArgumentValidator
    {
        /// <summary>
        /// Return the validated organization in the token
        /// </summary>
        /// <returns></returns>
        Task<Organization> ValidateCurrentOrganizationAsync();

        /// <summary>
        /// Return the location if valid within the current context
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        Task<Location> ValidateLocationAsync(Guid locationId);

        /// <summary>
        /// Return the product if valid within the current context
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<Product> ValidateProductAsync(Guid productId);

    }
}
