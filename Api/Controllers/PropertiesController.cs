using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Commands;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;
using Juliapos.Portal.ProductApi.Queries;
using Juliapos.Portal.ProductApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Juliapos.Portal.ProductApi.Api.Controllers
{
    /// <summary>
    /// Handle all property related requests
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public sealed class PropertiesController : ControllerBase
    {
        private readonly IEndpointArgumentValidator m_argumentValidator;
        private readonly ICommandHandler m_commandHandler;
        private readonly IQueryHandler m_queryHandler;
        private readonly IDtoMapper m_mapper;

        /// <summary>
        /// Create an instance of <see cref="PropertiesController"/>
        /// </summary>
        /// <param name="commandHandler"></param>
        /// <param name="queryHandler"></param>
        /// <param name="mapper"></param>
        /// <param name="endpointArgumentValidator"></param>
        public PropertiesController(
            ICommandHandler commandHandler,
            IQueryHandler queryHandler,
            IDtoMapper mapper,
            IEndpointArgumentValidator endpointArgumentValidator)
        {
            m_commandHandler = commandHandler;
            m_queryHandler = queryHandler;
            m_mapper = mapper;
            m_argumentValidator = endpointArgumentValidator;
        }

        /// <summary>
        /// Get properties
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetPropertiesAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the property.", typeof(IEnumerable<PropertyDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetPropertiesAsync()
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var Properties = await m_queryHandler.HandleQueryAsync<PropertiesQuery,IEnumerable<Property>>(
                new PropertiesQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                });
                
            var result = m_mapper.Map<Property, PropertyDto>(Properties);
            return Ok(result);
        }

        /// <summary>
        /// Get property with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(OperationId = "GetPropertyByIdAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the property.", typeof(PropertyDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the property was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<PropertyDto>> GetPropertyByIdAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var existingProperty = await m_queryHandler.HandleQueryAsync<PropertyQuery, Property>(
                new PropertyQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = m_mapper.Map<Property, PropertyDto>(existingProperty);
            return Ok(result);
        }

        /// <summary>
        /// Create a new property
        /// </summary>
        /// <param name="Property"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "CreatePropertyAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the new property.", typeof(PropertyDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another property.", typeof(ErrorResultDto))]
        public async Task<ActionResult<PropertyDto>> CreatePropertyAsync([FromBody] PropertyAddDto Property)
        {
            var id = Guid.NewGuid();
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var propertyToAdd = await m_commandHandler.HandleCommandAsync<PropertyCreateCommand, Property>(
                new PropertyCreateCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Name = Property.Name,
                    IdName = Property.IdName,
                    TypeName = Property.TypeName,
                    Enabled = Property.Enabled,
                });

            var result = m_mapper.Map<Property, PropertyDto>(propertyToAdd);
            return Ok(result);
        }


        /// <summary>
        /// Update a property
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Property"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(OperationId = "UpdatePropertyAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the property.", typeof(PropertyDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the property was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another property.", typeof(ErrorResultDto))]
        public async Task<ActionResult<PropertyDto>> UpdatePropertyAsync(Guid id, [FromBody] PropertyUpdateDto Property)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var property = await m_commandHandler.HandleCommandAsync<PropertyUpdateCommand, Property>(
                new PropertyUpdateCommand
                {
                    Id = id,
                    OrganizationId = validOrganization.OrganizationId,
                    Name = Property.Name,
                    TypeName = Property.TypeName,
                    Enabled = Property.Enabled,
                });

            var result = m_mapper.Map<Property, PropertyDto>(property);
            return Ok(result);
        }

        /// <summary>
        /// Delete a property
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(OperationId = "DeletePropertyAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the deleted property.", typeof(PropertyDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when the property is in use.", typeof(ErrorResultDto))]
        public async Task<ActionResult<PropertyDto>> DeletePropertyAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            
            var property = await m_commandHandler.HandleCommandAsync<PropertyDeleteCommand, Property>(
                new PropertyDeleteCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = property != null ? m_mapper.Map<Property, PropertyDto>(property) : null;
            return result == null ? Ok() : Ok(result);
        }


    }
}
