using Juliapos.AspNetCore.Authorization;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Services.Implementation
{
    /// <summary>
    /// Product category service
    /// </summary>
    public sealed class ProductCategoriesService : IProductCategoriesService
    {
        private readonly IApiDbDataStore m_dataStore;
        private readonly IAuthorizationContext m_authorizationContext;

        /// <summary>
        /// Create instance of type <see cref="ProductCategoriesService"/>
        /// </summary>
        /// <param name="dataStore"></param>
        /// <param name="authorizationContext"></param>
        public ProductCategoriesService(IApiDbDataStore dataStore, IAuthorizationContext authorizationContext)
        {
            m_dataStore = dataStore;
            m_authorizationContext = authorizationContext;
        }


        /// <inheritdoc />
        public async Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync(Guid? organizationId)
        {
            var query = m_dataStore.ProductCategoryDataQuery
                .WithOrganization();

            if (organizationId != null)
                query = query.WhereOrganizationId(organizationId.Value);

            var result = await query.ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> GetProductCategoryByIdAsync(Guid id)
        {
            var result = await m_dataStore.ProductCategoryDataQuery
                .WithOrganization()
                .WhereId(id)
                .SingleOrDefaultAsync();
            return result;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory)
        {
            m_dataStore.Add(productCategory);
            await m_dataStore.SaveChangesAsync();
            return productCategory;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            var existingCategory = await m_dataStore.ProductCategoryDataQuery
                .WithOrganization()
                .WhereId(productCategory.ProductCategoryId)
                .SingleOrDefaultAsync();

            if (!productCategory.Enabled && existingCategory.Enabled)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .WhereProductCategoryId(existingCategory.ProductCategoryId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.ProductCategoryHasProducts, existingCategory.ProductCategoryId);
            }

            existingCategory.Name = productCategory.Name;
            existingCategory.MeasureMethod = productCategory.MeasureMethod;
            existingCategory.DefaultForeColor = productCategory.DefaultForeColor;
            existingCategory.DefaultBackColor = productCategory.DefaultBackColor;
            existingCategory.Weight = productCategory.Weight;
            existingCategory.Enabled = productCategory.Enabled;
            await m_dataStore.SaveChangesAsync();

            return existingCategory;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> DeleteProductCategoryAsync(Guid id, Guid organizationId)
        {
            var result = await m_dataStore.ProductCategoryDataQuery
                .WhereId(id)
                .WhereOrganizationId(organizationId)
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
