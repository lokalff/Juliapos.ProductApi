using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Mappers
{
    /// <summary>
    /// Helpers to fill the custom attribute DTO object from the custom attribute
    /// </summary>
    public sealed class CustomAttributeDtoMapper : BaseDtoMapper<CustomAttribute, CustomAttributeDto>
    {
        /// <inheritdoc />
        public override CustomAttributeDto Map(CustomAttribute source)
        {
            var result = new CustomAttributeDto
            {
                Id = source.CustomAttributeId,
                IdName = source.IdName,
                Name = source.Name,
                TypeName = source.TypeName,
                Enabled = source.Enabled,
            };
            return result;
        }
    }
}
