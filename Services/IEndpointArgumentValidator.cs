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

        /// <summary>
        /// Return the product category if valid within the current context 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<ProductCategory> ValidateProductCategoryAsync(Guid categoryId);

        /// <summary>
        /// Return the dust category if valid within the current context 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<DustCategory> ValidateDustCategoryAsync(Guid categoryId);

        /// <summary>
        /// Return the menu category if valid within the current context 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<MenuCategory> ValidateMenuCategoryAsync(Guid categoryId);

        /// <summary>
        /// Return the custom attribute if valid within the current context 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        Task<CustomAttribute> ValidateCustomAttributeAsync(Guid attributeId);

        /// <summary>
        /// Return the product selectionpage if valid within the current context 
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        Task<SelectionPage> ValidateSelectionPageAsync(Guid pageId);

    }
}
