using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Mappers
{
    /// <summary>
    /// Helpers to fill the menu category DTO object from the product category
    /// </summary>
    public sealed class SelectionPageDtoMapper : BaseDtoMapper<SelectionPage, SelectionPageDto>
    {
        /// <inheritdoc />
        public override SelectionPageDto Map(SelectionPage source)
        {
            var result = new SelectionPageDto
            {
                Id = source.SelectionPageId,
                IdName = source.IdName,
                Name = source.Name,
                Weight = source.Weight,
                Enabled = source.Enabled,
            };
            return result;
        }
    }
}
