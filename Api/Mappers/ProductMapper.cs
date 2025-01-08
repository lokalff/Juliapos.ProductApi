using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Mappers
{
    public sealed class ProductMapper : BaseDtoMapper<Product, ProductDto>
    {
        public override ProductDto Map(Product source)
        {
            var result = new ProductDto
            {
                Name = source.Name,
                Code = source.Code,
                MenuName = source.MenuName,
                VatLevel = source.VatLevel,
                Description = source.Description,
                InInventory = source.InInventory,
                Percentage = source.Percentage,
                AscendingStock = source.AscendingStock,
                State = source.State,
                Created = source.Created,
                Updated = source.Updated,
                UserCreate = source.UserCreate,
                UserUpdate = source.UserUpdate
            };

            if (source.PropertieValues != null && source.PropertieValues.Any())
            {
                result.Properties = source.PropertieValues.Select(x => new PropertyReferenceDto
                {
                    Name = x.Property.Name,
                    IdName = x.Property.IdName,
                    TypeName = x.Property.TypeName,
                    Value = x.Value
                }).ToArray();
            }

            if (source.ProductVariations != null && source.ProductVariations.Any())
            {
                result.Variations = source.ProductVariations.Select(x => new ProductVariationReferenceDto
                {
                    Name = x.Name,
                    Code = x.Code,
                    Sku = x.Sku,
                    ProductVariationLocations = x.ProductVariationLocations.Select(y => new ProductVariationLocationReferenceDto
                    {
                        Status = y.Status,
                        NextStatus = y.NextStatus,
                        ChangeDateTime = y.ChangeDateTime,
                        UnitPrice = y.UnitPrice,
                        UnitPricePurchase = y.UnitPricePurchase,
                        MaxAmount = y.MaxAmount,
                        MinAmount = y.MinAmount,
                        OnMenuEnd = y.OnMenuEnd,
                        OnMenuStart = y.OnMenuStart,
                        ShowOnFavoritePage = y.ShowOnFavoritePage,
                        Transport = y.Transport
                    }).ToArray()
                }).ToArray();
            }
            return result;
        }
    }
}
