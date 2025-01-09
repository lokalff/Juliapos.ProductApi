using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Services.Implementation
{
    public static class ProductExtensions
    {
        public static void SyncProperties(this Product existingProduct, IEnumerable<PropertyValue> properties)
        {
            // Add or update properties
            foreach (var property in properties.ToList())
            {
                var existingProperty = existingProduct.PropertieValues
                    .FirstOrDefault(p => p.PropertyId == property.PropertyId);

                if (existingProperty == null)
                    existingProduct.PropertieValues.Add(property);
                else
                    existingProperty.Value = property.Value;
            }

            // Remove deleted properties
            foreach (var property in existingProduct.PropertieValues.ToList())
            {
                if (properties.All(p => p.PropertyId != property.PropertyId))
                    existingProduct.PropertieValues.Remove(property);
            }
        }

        public static void SyncVariations(this Product existingProduct, IEnumerable<ProductVariation> variations)
        {
            // Add or update variations
            foreach (var variation in variations.ToList())
            {
                var existingVariation = existingProduct.ProductVariations
                    .FirstOrDefault(p => p.ProductVariationId == variation.ProductVariationId);

                if (existingVariation == null)
                    existingProduct.ProductVariations.Add(variation);
                else
                {
                    existingVariation.Sku = variation.Sku;
                    existingVariation.Code = variation.Code;
                    existingVariation.Name = variation.Name;

                    existingVariation.SyncVariationLocations(variation.ProductVariationLocations);
                }
            }

            // Remove deleted variations
            foreach (var variation in existingProduct.ProductVariations.ToList())
            {
                if (variations.All(p => p.ProductVariationId != variation.ProductVariationId))
                    existingProduct.ProductVariations.Remove(variation);
            }
        }


        public static void SyncVariationLocations(this ProductVariation existingVariation, IEnumerable<ProductVariationLocation> variationLocations)
        {
            foreach (var variationLocation in variationLocations.ToList())
            {
                var existingVariationLocation = existingVariation.ProductVariationLocations
                    .FirstOrDefault(p => p.LocationId == variationLocation.LocationId);

                if (existingVariationLocation == null)
                    existingVariation.ProductVariationLocations.Add(variationLocation);
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
                if (variationLocations.All(p => p.ProductVariationLocationId != variationLocation.ProductVariationLocationId))
                    existingVariation.ProductVariationLocations.Remove(variationLocation);
            }
        }

    }
}
