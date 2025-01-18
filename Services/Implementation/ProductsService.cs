using Juliapos.AspNetCore.Authorization;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Services.Implementation
{
    public sealed class ProductsService : IProductsService
    {
        private readonly IApiDbDataStore m_dataStore;
        private readonly IAuthorizationContext m_authorizationContext;

        /// <summary>
        /// Create instance of type <see cref="ProductsService"/>
        /// </summary>
        /// <param name="dataStore"></param>
        /// <param name="authorizationContext"></param>
        public ProductsService(IApiDbDataStore dataStore, IAuthorizationContext authorizationContext)
        {
            m_dataStore = dataStore;
            m_authorizationContext = authorizationContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Product>> GetProductsAsync(Guid? organizationId)
        {
            var query = m_dataStore.ProductDataQuery
                .WithLocations()
                .WithProperties()
                .WithVariations();

            if (organizationId != null)
                query = query.WhereOrganizationId(organizationId.Value);

            var result = await query.ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var result = await m_dataStore.ProductDataQuery
                .WithLocations()
                .WithProperties()
                .WithVariations()
                .WhereId(id)
                .WhereNotDeleted()
                .SingleOrDefaultAsync();

            if (result != null)
            {
                foreach (var variation in result.ProductVariations.ToList())
                {
                    if (variation.ProductVariationLocations.All(vl => vl.LocationId != null))
                        variation.ProductVariationLocations.Add(new ProductVariationLocation());
                }
            }
            return result;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CheckProduct();

            product.Created = DateTime.UtcNow;
            product.UserUpdate = m_authorizationContext.UserId.ToString();

            m_dataStore.Add(product);
            await m_dataStore.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var existing = await m_dataStore.ProductDataQuery
                .WithLocations()
                .WithProperties()
                .WithVariations()
                .WhereId(product.ProductId)
                .SingleOrDefaultAsync();

            existing.ProductCategoryId = product.ProductCategoryId;
            existing.DustCategoryId = product.DustCategoryId;
            existing.MenuCategoryId = product.MenuCategoryId;

            existing.Code = product.Code;
            existing.Name = product.Name;
            existing.MenuName = product.MenuName;
            existing.VatLevel = product.VatLevel;
            existing.Description = product.Description;
            existing.InInventory = product.InInventory;
            existing.Percentage = product.Percentage;
            existing.AscendingStock = product.AscendingStock;
            existing.State = product.State;
            existing.Updated = DateTime.UtcNow;
            existing.UserUpdate = m_authorizationContext.UserId.ToString();

            existing.SyncProperties(product.PropertieValues);
            existing.SyncVariations(product.ProductVariations);

            product.CheckProduct();

            await m_dataStore.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Guid id, bool purge)
        {
            var result = await m_dataStore.ProductDataQuery
                .WithLocations()
                .WithProperties()
                .WithVariations()
                .WhereId(id)
                .SingleOrDefaultAsync();

            if (result != null)
            {
                if (purge)
                    m_dataStore.Remove(result);
                else
                {
                    result.Deleted = DateTime.UtcNow;
                    result.UserDelete = m_authorizationContext.UserId.ToString();
                }
                await m_dataStore.SaveChangesAsync();
            }
            return result;
        }

    }

}
