﻿using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Mappers
{
    /// <summary>
    /// Helpers to fill the product DTO object from the product
    /// </summary>
    public sealed class ProductDtoMapper : BaseDtoMapper<Product, ProductDto>
    {
        /// <inheritdoc />
        public override ProductDto Map(Product source)
        {
            var result = new ProductDto
            {
                Id = source.ProductId,
                ProductCategoryId = source.ProductCategoryId,
                DustCategoryId = source.DustCategoryId,
                MenuCategoryId = source.MenuCategoryId,
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

            if (source.CustomAttributeValues != null && source.CustomAttributeValues.Any())
            {
                result.CustomAttributes = source.CustomAttributeValues.Select(x => new ProductCustomAttributeDto
                {
                    Id = x.CustomAttributeId,
                    Value = x.Value
                }).ToArray();
            }

            if (source.ProductVariations != null && source.ProductVariations.Any())
            {
                result.Variations = source.ProductVariations.Select(x => new ProductVariationDto
                {
                    Name = x.NameExtension,
                    Code = x.CodeExtension,
                    Sku = x.Sku,
                    ProductVariationLocations = x.ProductVariationLocations.Select(y => new ProductVariationLocationDto
                    {
                        LocationId = y.LocationId,
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
