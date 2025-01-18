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
    /// Handle all dust category related requests
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public sealed class DustCategoriesController : ControllerBase
    {
        private readonly IEndpointArgumentValidator m_argumentValidator;
        private readonly ICommandHandler m_commandHandler;
        private readonly IQueryHandler m_queryHandler;
        private readonly IDtoMapper m_mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandHandler"></param>
        /// <param name="queryHandler"></param>
        /// <param name="mapper"></param>
        /// <param name="endpointArgumentValidator"></param>
        public DustCategoriesController(
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
        /// Get dust categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetDustCategoriesAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the dust category.", typeof(IEnumerable<DustCategoryDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<IEnumerable<DustCategoryDto>>> GetDustCategoriesAsync()
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var dustCategories = await m_queryHandler.HandleQueryAsync<DustCategoriesQuery,IEnumerable<DustCategory>>(
                new DustCategoriesQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                });
                
            var result = m_mapper.Map<DustCategory, DustCategoryDto>(dustCategories);
            return Ok(result);
        }

        /// <summary>
        /// Get dust category with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(OperationId = "GetDustCategoryByIdAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the dust category.", typeof(DustCategoryDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the dust category was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<DustCategoryDto>> GetDustCategoryByIdAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var existingDustCategory = await m_queryHandler.HandleQueryAsync<DustCategoryQuery, DustCategory>(
                new DustCategoryQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = m_mapper.Map<DustCategory, DustCategoryDto>(existingDustCategory);
            return Ok(result);
        }

        /// <summary>
        /// Create a new dust category
        /// </summary>
        /// <param name="dustCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateDustCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the new dust category.", typeof(DustCategoryDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another dust category.", typeof(ErrorResultDto))]
        public async Task<ActionResult<DustCategoryDto>> CreateDustCategoryAsync([FromBody] DustCategoryAddDto dustCategory)
        {
            var id = Guid.NewGuid();
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var categoryToAdd = await m_commandHandler.HandleCommandAsync<DustCategoryCreateCommand, DustCategory>(
                new DustCategoryCreateCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Name = dustCategory.Name,
                    Weight = dustCategory.Weight,
                });

            var result = m_mapper.Map<DustCategory, DustCategoryDto>(categoryToAdd);
            return Ok(result);
        }


        /// <summary>
        /// Update a dust category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dustCategory"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(OperationId = "UpdateDustCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the dust category.", typeof(DustCategoryDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another dust category.", typeof(ErrorResultDto))]
        public async Task<ActionResult<DustCategoryDto>> UpdateDustCategoryAsync(Guid id, [FromBody] DustCategoryUpdateDto dustCategory)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var category = await m_commandHandler.HandleCommandAsync<DustCategoryUpdateCommand, DustCategory>(
                new DustCategoryUpdateCommand
                {
                    Id = id,
                    OrganizationId = validOrganization.OrganizationId,
                    Name = dustCategory.Name,
                    Weight = dustCategory.Weight,
                });

            var result = m_mapper.Map<DustCategory, DustCategoryDto>(category);
            return Ok(result);
        }

        /// <summary>
        /// Delete a menu category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(OperationId = "DeleteDustCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the deleted dust category.", typeof(DustCategoryDto))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Returned when record was no longer present.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when the category is not empty.", typeof(ErrorResultDto))]
        public async Task<ActionResult<DustCategoryDto>> DeleteMenuCategoryAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            
            var category = await m_commandHandler.HandleCommandAsync<DustCategoryDeleteCommand, DustCategory>(
                new DustCategoryDeleteCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = category != null ? m_mapper.Map<DustCategory, DustCategoryDto>(category) : null;
            return Ok(result);
        }


    }
}
