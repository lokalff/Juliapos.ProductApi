using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Mappers
{
    /// <summary>
    /// Helpers to fill the product category DTO object from the product category
    /// </summary>
    public sealed class ProductCategoryDtoMapper : BaseDtoMapper<ProductCategory, ProductCategoryDto>
    {
        /// <inheritdoc />
        public override ProductCategoryDto Map(ProductCategory source)
        {
            var result = new ProductCategoryDto
            {
                Id = source.ProductCategoryId,
                IdName = source.IdName,
                Name = source.Name,
                Weight = source.Weight,
                Enabled = source.Enabled,
                MeasureMethod = source.MeasureMethod,
                DefaultBackColor = source.DefaultBackColor,
                DefaultForeColor = source.DefaultForeColor,
            };
            return result;
        }
    }
}
