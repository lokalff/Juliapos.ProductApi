using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Controllers
{
    internal static class ProductDtoExtensions
    {

        /// <summary>
        /// Get PropertyValue objects from the <see cref="ProductPropertyDto"/>
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static PropertyValue MapProperty(this ProductPropertyDto property)
        {
            var result = new PropertyValue
            {
                PropertyId = property.Id,
                Value = property.Value,
            };
            return result;
        }

        /// <summary>
        /// Get ProductVariation objects from the <see cref="ProductVariationAddDto"/>
        /// </summary>
        /// <param name="prdvar"></param>
        /// <returns></returns>
        public static ProductVariation MapVariationAdd(this ProductVariationAddDto prdvar)
        {
            var result = new ProductVariation
            {
                CodeExtension = prdvar.Code,
                NameExtension = prdvar.Name,
                Sku = prdvar.Sku,
                ProductVariationLocations = prdvar.ProductVariationLocations != null ?
                    prdvar.ProductVariationLocations.Select(vl => new ProductVariationLocation
                    {
                        LocationId = vl.LocationId,
                        UnitPrice = vl.UnitPrice,
                        UnitPricePurchase = vl.UnitPricePurchase,
                        ShowOnFavoritePage = vl.ShowOnFavoritePage,
                        MaxAmount = vl.MaxAmount,
                        MinAmount = vl.MinAmount,
                        Transport = vl.Transport,
                        Status = vl.Status,
                        NextStatus = vl.NextStatus,
                        ChangeDateTime = vl.ChangeDateTime,
                        OnMenuEnd = vl.OnMenuEnd,
                        OnMenuStart = vl.OnMenuStart,
                    }).ToList() : new List<ProductVariationLocation>()
            };
            return result;
        }

        public static SelectionPageProduct MapProductSelectionPage(this ProductSelectionPageReferenceDto selectionPage)
        {
            var result = new SelectionPageProduct
            {
                BackColor = selectionPage.BackColor,
                ForeColor = selectionPage.ForeColor,
                ColumnIdx = selectionPage.ColumnIdx,
                RowIdx = selectionPage.RowIdx,
                SelectionPageId = selectionPage.SelectionPageId,
            };

            return result;
        }

        /// <summary>
        /// Get product object from a <see cref="ProductAddDto"/>
        /// </summary>
        /// <param name="product"></param>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Product MapProductAdd(this ProductAddDto product, Guid id)
        {

            var propertyValuesToAdd = product.Properties?.Select(p => p.MapProperty()).ToList();
            var variationsToAdd = product.Variations?.Select(v => v.MapVariationAdd()).ToList();
            var selectionPageToAdd = product.SelectionPages.Select(s => s.MapProductSelectionPage()).ToList();

            var result = new Product
            {
                ProductId = id,
                ProductCategoryId = product.ProductCategoryId,
                DustCategoryId = product.DustCategoryId,
                MenuCategoryId = product.MenuCategoryId,

                Code = product.Code,
                Name = product.Name,
                MenuName = product.MenuName,
                VatLevel = product.VatLevel,
                Description = product.Description,
                InInventory = product.InInventory,
                Percentage = product.Percentage,
                AscendingStock = product.AscendingStock,
                State = product.State,

                //Created = dateTime,           Set in the save to db
                //UserCreate = userName,

                PropertieValues = propertyValuesToAdd,
                ProductVariations = variationsToAdd,
                SelectionPageProducts = selectionPageToAdd,
            };

            return result;
        }


        /// <summary>
        /// Get ProductVariation objects from the <see cref="ProductVariationAddDto"/>
        /// </summary>
        /// <param name="prdvar"></param>
        /// <returns></returns>
        public static ProductVariation MapVariationUpdate(this ProductVariationUpdateDto prdvar)
        {
            var result = new ProductVariation
            {
                ProductVariationId = prdvar.Id,
                CodeExtension = prdvar.Code,
                NameExtension = prdvar.Name,
                Sku = prdvar.Sku,
                ProductVariationLocations = prdvar.ProductVariationLocations != null ?
                    prdvar.ProductVariationLocations.Select(vl => new ProductVariationLocation
                    {
                        LocationId = vl.LocationId,
                        UnitPrice = vl.UnitPrice,
                        UnitPricePurchase = vl.UnitPricePurchase,
                        ShowOnFavoritePage = vl.ShowOnFavoritePage,
                        MaxAmount = vl.MaxAmount,
                        MinAmount = vl.MinAmount,
                        Transport = vl.Transport,
                        Status = vl.Status,
                        NextStatus = vl.NextStatus,
                        ChangeDateTime = vl.ChangeDateTime,
                        OnMenuEnd = vl.OnMenuEnd,
                        OnMenuStart = vl.OnMenuStart,
                    }).ToList() : new List<ProductVariationLocation>()
            };
            return result;
        }

        public static Product MapProductUpdate(this ProductUpdateDto product, Guid id)
        {
            var propertyValuesToAdd = product.Properties?.Select(p => p.MapProperty()).ToList();
            var variationsToAdd = product.Variations?.Select(v => v.MapVariationUpdate()).ToList();
            var selectionPageToAdd = product.SelectionPages.Select(s => s.MapProductSelectionPage()).ToList();

            var result = new Product
            {
                ProductId = id,
                ProductCategoryId = product.ProductCategoryId,
                DustCategoryId = product.DustCategoryId,
                MenuCategoryId = product.MenuCategoryId,

                Code = product.Code,
                Name = product.Name,
                MenuName = product.MenuName,
                VatLevel = product.VatLevel,
                Description = product.Description,
                InInventory = product.InInventory,
                Percentage = product.Percentage,
                AscendingStock = product.AscendingStock,
                State = product.State,

                //Created = dateTime,               Happens in the save to db
                //UserCreate = userName,

                PropertieValues = propertyValuesToAdd,
                ProductVariations = variationsToAdd,
                SelectionPageProducts = selectionPageToAdd,
            };

            return result;
        }

    }
}
