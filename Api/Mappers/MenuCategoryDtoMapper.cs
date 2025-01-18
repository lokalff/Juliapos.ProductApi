using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Mappers
{
    /// <summary>
    /// Helpers to fill the menu category DTO object from the product category
    /// </summary>
    public sealed class MenuCategoryDtoMapper : BaseDtoMapper<MenuCategory, MenuCategoryDto>
    {
        /// <inheritdoc />
        public override MenuCategoryDto Map(MenuCategory source)
        {
            var result = new MenuCategoryDto
            {
                Id = source.MenuCategoryId,
                IdName = source.IdName,
                Name = source.Name,
                Weight = source.Weight,
                Enabled = source.Enabled,
            };
            return result;
        }
    }
}
