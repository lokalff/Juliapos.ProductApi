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
    /// Handle all custom attributes related requests
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public sealed class CustomAttributesController : ControllerBase
    {
        private readonly IEndpointArgumentValidator m_argumentValidator;
        private readonly ICommandHandler m_commandHandler;
        private readonly IQueryHandler m_queryHandler;
        private readonly IDtoMapper m_mapper;

        /// <summary>
        /// Create an instance of <see cref="CustomAttributesController"/>
        /// </summary>
        /// <param name="commandHandler"></param>
        /// <param name="queryHandler"></param>
        /// <param name="mapper"></param>
        /// <param name="endpointArgumentValidator"></param>
        public CustomAttributesController(
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
        /// Get custom attributes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetCustomAttributesAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the custom attributes.", typeof(IEnumerable<CustomAttributeDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<IEnumerable<CustomAttributeDto>>> GetCustomAttributesAsync()
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var attributes = await m_queryHandler.HandleQueryAsync<CustomAttributesQuery,IEnumerable<CustomAttribute>>(
                new CustomAttributesQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                });
                
            var result = m_mapper.Map<CustomAttribute, CustomAttributeDto>(attributes);
            return Ok(result);
        }

        /// <summary>
        /// Get custom attribute with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(OperationId = "GetCustomAttributeByIdAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the custom attribute.", typeof(CustomAttributeDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the custom attribute was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<CustomAttributeDto>> GetCustomAttributeByIdAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var existingAttribute = await m_queryHandler.HandleQueryAsync<CustomAttributeQuery, CustomAttribute>(
                new CustomAttributeQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = m_mapper.Map<CustomAttribute, CustomAttributeDto>(existingAttribute);
            return Ok(result);
        }

        /// <summary>
        /// Create a new custom attribute
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateCustomAttributeAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the new custom attribute.", typeof(CustomAttributeDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another custom attribute.", typeof(ErrorResultDto))]
        public async Task<ActionResult<CustomAttributeDto>> CreateCustomAttributeAsync([FromBody] CustomAttributeAddDto attribute)
        {
            var id = Guid.NewGuid();
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var attributeToAdd = await m_commandHandler.HandleCommandAsync<CustomAttributeCreateCommand, CustomAttribute>(
                new CustomAttributeCreateCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Name = attribute.Name,
                    IdName = attribute.IdName,
                    TypeName = attribute.TypeName,
                    Enabled = attribute.Enabled,
                });

            var result = m_mapper.Map<CustomAttribute, CustomAttributeDto>(attributeToAdd);
            return Ok(result);
        }


        /// <summary>
        /// Update a custom attribute
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(OperationId = "UpdateCustomAttributeAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the custom attribute.", typeof(CustomAttributeDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the custom attribute was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another custom attribute.", typeof(ErrorResultDto))]
        public async Task<ActionResult<CustomAttributeDto>> UpdateCustomAttributeAsync(Guid id, [FromBody] CustomAttributeUpdateDto attribute)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var updatedAttribute = await m_commandHandler.HandleCommandAsync<CustomAttributeUpdateCommand, CustomAttribute>(
                new CustomAttributeUpdateCommand
                {
                    Id = id,
                    OrganizationId = validOrganization.OrganizationId,
                    Name = attribute.Name,
                    TypeName = attribute.TypeName,
                    Enabled = attribute.Enabled,
                });

            var result = m_mapper.Map<CustomAttribute, CustomAttributeDto>(updatedAttribute);
            return Ok(result);
        }

        /// <summary>
        /// Delete a custom attribute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(OperationId = "DeleteCustomAttributeAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the deleted custom attribute.", typeof(CustomAttributeDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when the custom attribute is in use.", typeof(ErrorResultDto))]
        public async Task<ActionResult<CustomAttributeDto>> DeleteCustomAttributeAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            
            var attribute = await m_commandHandler.HandleCommandAsync<CustomAttributeDeleteCommand, CustomAttribute>(
                new CustomAttributeDeleteCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = attribute != null ? m_mapper.Map<CustomAttribute, CustomAttributeDto>(attribute) : null;
            return result == null ? Ok() : Ok(result);
        }


    }
}
