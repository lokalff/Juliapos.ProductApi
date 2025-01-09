using System;
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
        public ProductsService(IApiDbDataStore dataStore, IAuthorizationContext authorizationContext)
        {
            m_dataStore = dataStore;
            m_authorizationContext = authorizationContext;
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

            // Add or update properties
            foreach(var property in product.PropertieValues.ToList())
            {
                var existingProperty = existing.PropertieValues
                    .FirstOrDefault(p => p.PropertyId == property.PropertyId);

                if (existingProperty == null)
                    existing.PropertieValues.Add(property);
                else
                    existingProperty.Value = property.Value;
            }
            
            // Remove deleted properties
            foreach(var property in existing.PropertieValues.ToList())
            {
                if (product.PropertieValues.All(p => p.PropertyId != property.PropertyId))
                    existing.PropertieValues.Remove(property);
            }


            // Add or update variations
            foreach (var variation in product.ProductVariations.ToList())
            {
                var existingVariation = existing.ProductVariations
                    .FirstOrDefault(p => p.ProductVariationId == variation.ProductVariationId);

                if (existingVariation == null)
                    existing.ProductVariations.Add(variation);
                else
                {
                    existingVariation.Sku = variation.Sku;
                    existingVariation.Code = variation.Code;
                    existingVariation.Name = variation.Name;

                    foreach (var variationLocation in variation.ProductVariationLocations.ToList())
                    {
                        var existingVariationLocation = existingVariation.ProductVariationLocations
                            .FirstOrDefault(p => p.LocationId == variationLocation.LocationId);

                        if (existingVariationLocation == null)
                            variation.ProductVariationLocations.Add(variationLocation);
                        else
                        {
                            existingVariationLocation.UnitPrice = variationLocation.UnitPrice;
                            existingVariationLocation.UnitPricePurchase = variationLocation.UnitPricePurchase;
                            existingVariationLocation.ShowOnFavoritePage = variationLocation.ShowOnFavoritePage;
                            existingVariationLocation.MinAmount = variationLocation.MinAmount;
                            existingVariationLocation.MaxAmount = variationLocation.MaxAmount;
                            existingVariationLocation.Transport = variationLocation.Transport;
                            existingVariationLocation.Status = variationLocation.Status;
                            existingVariationLocation.NextStatus = variationLocation.NextStatus;
                            existingVariationLocation.ChangeDateTime = variationLocation.ChangeDateTime;
                            existingVariationLocation.OnMenuStart = variationLocation.OnMenuStart;
                            existingVariationLocation.OnMenuEnd = variationLocation.OnMenuEnd;
                        }
                    }

                    // Remove deleted variation locations
                    foreach (var variationLocation in existingVariation.ProductVariationLocations.ToList())
                    {
                        if (variation.ProductVariationLocations.All(p => p.ProductVariationLocationId != variationLocation.ProductVariationLocationId))
                            existingVariation.ProductVariationLocations.Remove(variationLocation);
                    }

                }
            }

            // Remove deleted variations
            foreach (var variation in existing.ProductVariations.ToList())
            {
                if (product.ProductVariations.All(p => p.ProductVariationId != variation.ProductVariationId))
                    existing.ProductVariations.Remove(variation);
            }

            await m_dataStore.SaveChangesAsync();
            return product;
        }

    }

}
