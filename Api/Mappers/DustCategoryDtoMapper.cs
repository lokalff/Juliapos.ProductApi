using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Mappers
{
    /// <summary>
    /// Helpers to fill the dust category DTO object from the product category
    /// </summary>
    public sealed class DustCategoryDtoMapper : BaseDtoMapper<DustCategory, DustCategoryDto>
    {
        /// <inheritdoc />
        public override DustCategoryDto Map(DustCategory source)
        {
            var result = new DustCategoryDto
            {
                Id = source.DustCategoryId,
                Name = source.Name,
                Weight = source.Weight,
            };
            return result;
        }
    }
}
