using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Services.Implementation
{
    public static class ProductExtensions
    {
        public static void CheckProduct(this Product product)
        {
            foreach (var variation in product.ProductVariations)
            {
                var locationDuplicates = variation.ProductVariationLocations
                    .GroupBy(k => k.LocationId)
                    .Where(g => g.Count() > 1)
                    .ToList();

                if (locationDuplicates.Any())
                {
                    var locationsString = string.Join(',', locationDuplicates.Select(g => g.Key?.ToString() ?? "null"));
                    throw new HttpBadRequestException(ApiErrorCode.ProductVariationDuplicateLocation,
                        product.ProductId, $"locations {locationsString}");
                }
            }
        }

        public static void SyncCustomAttributes(this Product existingProduct, IEnumerable<CustomAttributeValue> attributes)
        {
            // Add or update attributes
            foreach (var property in attributes.ToList())
            {
                var existingAttribute = existingProduct.CustomAttributeValues
                    .FirstOrDefault(p => p.CustomAttributeId == property.CustomAttributeId);

                if (existingAttribute == null)
                    existingProduct.CustomAttributeValues.Add(property);
                else
                    existingAttribute.Value = property.Value;
            }

            // Remove deleted attributes
            foreach (var attribute in existingProduct.CustomAttributeValues.ToList())
            {
                if (attributes.All(p => p.CustomAttributeId != attribute.CustomAttributeId))
                    existingProduct.CustomAttributeValues.Remove(attribute);
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
                    existingVariation.CodeExtension = variation.CodeExtension;
                    existingVariation.NameExtension = variation.NameExtension;

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
