using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Mappers
{
    /// <summary>
    /// Helpers to fill the proerty DTO object from the property
    /// </summary>
    public sealed class PropertyDtoMapper : BaseDtoMapper<Property, PropertyDto>
    {
        /// <inheritdoc />
        public override PropertyDto Map(Property source)
        {
            var result = new PropertyDto
            {
                Id = source.PropertyId,
                IdName = source.IdName,
                Name = source.Name,
                TypeName = source.TypeName,
                Enabled = source.Enabled,
            };
            return result;
        }
    }
}
