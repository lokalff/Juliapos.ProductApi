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
    /// Handle all product selection page related requests
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public sealed class SelectionPagesController : ControllerBase
    {
        private readonly IEndpointArgumentValidator m_argumentValidator;
        private readonly ICommandHandler m_commandHandler;
        private readonly IQueryHandler m_queryHandler;
        private readonly IDtoMapper m_mapper;

        /// <summary>
        /// Create an instance of <see cref="SelectionPagesController"/>
        /// </summary>
        /// <param name="commandHandler"></param>
        /// <param name="queryHandler"></param>
        /// <param name="mapper"></param>
        /// <param name="endpointArgumentValidator"></param>
        public SelectionPagesController(
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
        /// Get product selection pages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetSelectionPagesAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product selection page.", typeof(IEnumerable<SelectionPageDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<IEnumerable<SelectionPageDto>>> GetSelectionPagesAsync()
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var selectionPages = await m_queryHandler.HandleQueryAsync<SelectionPagesQuery,IEnumerable<SelectionPage>>(
                new SelectionPagesQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                });
                
            var result = m_mapper.Map<SelectionPage, SelectionPageDto>(selectionPages);
            return Ok(result);
        }

        /// <summary>
        /// Get product selection page with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(OperationId = "GetSelectionPageByIdAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product selection page.", typeof(SelectionPageDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the product selection page was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<SelectionPageDto>> GetSelectionPageByIdAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var existingSelectionPage = await m_queryHandler.HandleQueryAsync<SelectionPageQuery, SelectionPage>(
                new SelectionPageQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = m_mapper.Map<SelectionPage, SelectionPageDto>(existingSelectionPage);
            return Ok(result);
        }

        /// <summary>
        /// Create a new product selection page
        /// </summary>
        /// <param name="selectionPage"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateSelectionPageAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the new product selection page.", typeof(SelectionPageDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another product selection page.", typeof(ErrorResultDto))]
        public async Task<ActionResult<SelectionPageDto>> CreateSelectionPageAsync([FromBody] SelectionPageAddDto selectionPage)
        {
            var id = Guid.NewGuid();
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var categoryToAdd = await m_commandHandler.HandleCommandAsync<SelectionPageCreateCommand, SelectionPage>(
                new SelectionPageCreateCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Name = selectionPage.Name,
                    IdName = selectionPage.IdName,
                    Weight = selectionPage.Weight,
                    Enabled = selectionPage.Enabled,
                });

            var result = m_mapper.Map<SelectionPage, SelectionPageDto>(categoryToAdd);
            return Ok(result);
        }


        /// <summary>
        /// Update a product selection page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="selectionPage"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(OperationId = "UpdateSelectionPageAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product selection page.", typeof(SelectionPageDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the product selection page was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another product selection page.", typeof(ErrorResultDto))]
        public async Task<ActionResult<SelectionPageDto>> UpdateSelectionPageAsync(Guid id, [FromBody] SelectionPageUpdateDto selectionPage)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var category = await m_commandHandler.HandleCommandAsync<SelectionPageUpdateCommand, SelectionPage>(
                new SelectionPageUpdateCommand
                {
                    Id = id,
                    OrganizationId = validOrganization.OrganizationId,
                    Name = selectionPage.Name,
                    Weight = selectionPage.Weight,
                    Enabled = selectionPage.Enabled,
                });

            var result = m_mapper.Map<SelectionPage, SelectionPageDto>(category);
            return Ok(result);
        }

        /// <summary>
        /// Delete a product selection page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(OperationId = "DeleteSelectionPageAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the deleted menu category.", typeof(SelectionPageDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when the category is not empty.", typeof(ErrorResultDto))]
        public async Task<ActionResult<SelectionPageDto>> DeleteSelectionPageAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            
            var category = await m_commandHandler.HandleCommandAsync<SelectionPageDeleteCommand, SelectionPage>(
                new SelectionPageDeleteCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = category != null ? m_mapper.Map<SelectionPage, SelectionPageDto>(category) : null;
            return result == null ? Ok() : Ok(result);
        }


    }
}
