using System.Linq;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Services.Implementation
{
    public sealed class ProductsService : IProductsService
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create instance of type <see cref="ProductsService"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public ProductsService(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }


        /// <inheritdoc />
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var result = await m_dataStore.ProductDataQuery
                .WithLocations()
                .WithProperties()
                .WithVariations()
                .WhereId(id)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
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
            existing.Updated = product.Updated;
            existing.UserUpdate = product.UserUpdate;

            existing.SyncProperties(product.PropertieValues);
            existing.SyncVariations(product.ProductVariations);

            // Dit moet in een ProductValidator class (in Db? or misschien Services)
            // Omdat we hier in existing... zoeken, kan de duplicate check direct met een SelectMany
            foreach (var variation in existing.ProductVariations)
            {
                var locationDuplicates = variation.ProductVariationLocations
                    .GroupBy(k => k.LocationId)
                    .Where(g => g.Count() > 1)
                    .ToList();

                if (locationDuplicates.Any())
                {
                    var locationsString = string.Join(',', locationDuplicates.Select(g => g.Key?.ToString() ?? "null"));
                    throw new HttpBadRequestException(ApiErrorCode.ProductVariationDuplicateLocation,
                        existing.ProductId, $"locations {locationsString}");
                }
            }

            await m_dataStore.SaveChangesAsync();
            return product;
        }

    }

}
