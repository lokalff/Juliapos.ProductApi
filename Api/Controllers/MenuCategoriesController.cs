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
    /// Handle all menu category related requests
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public sealed class MenuCategoriesController : ControllerBase
    {
        private readonly IEndpointArgumentValidator m_argumentValidator;
        private readonly ICommandHandler m_commandHandler;
        private readonly IQueryHandler m_queryHandler;
        private readonly IDtoMapper m_mapper;

        /// <summary>
        /// Create an instance of <see cref="MenuCategoriesController"/>
        /// </summary>
        /// <param name="commandHandler"></param>
        /// <param name="queryHandler"></param>
        /// <param name="mapper"></param>
        /// <param name="endpointArgumentValidator"></param>
        public MenuCategoriesController(
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
        /// Get menu categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetMenuCategoriesAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the menu category.", typeof(IEnumerable<MenuCategoryDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<IEnumerable<MenuCategoryDto>>> GetMenuCategoriesAsync()
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var MenuCategories = await m_queryHandler.HandleQueryAsync<MenuCategoriesQuery,IEnumerable<MenuCategory>>(
                new MenuCategoriesQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                });
                
            var result = m_mapper.Map<MenuCategory, MenuCategoryDto>(MenuCategories);
            return Ok(result);
        }

        /// <summary>
        /// Get menu category with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(OperationId = "GetMenuCategoryByIdAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the menu category.", typeof(MenuCategoryDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the menu category was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<MenuCategoryDto>> GetMenuCategoryByIdAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var existingMenuCategory = await m_queryHandler.HandleQueryAsync<MenuCategoryQuery, MenuCategory>(
                new MenuCategoryQuery
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = m_mapper.Map<MenuCategory, MenuCategoryDto>(existingMenuCategory);
            return Ok(result);
        }

        /// <summary>
        /// Create a new menu category
        /// </summary>
        /// <param name="menuCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateMenuCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the new menu category.", typeof(MenuCategoryDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another menu category.", typeof(ErrorResultDto))]
        public async Task<ActionResult<MenuCategoryDto>> CreateMenuCategoryAsync([FromBody] MenuCategoryAddDto menuCategory)
        {
            var id = Guid.NewGuid();
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var categoryToAdd = await m_commandHandler.HandleCommandAsync<MenuCategoryCreateCommand, MenuCategory>(
                new MenuCategoryCreateCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Name = menuCategory.Name,
                    IdName = menuCategory.IdName,
                    Weight = menuCategory.Weight,
                    Enabled = menuCategory.Enabled,
                });

            var result = m_mapper.Map<MenuCategory, MenuCategoryDto>(categoryToAdd);
            return Ok(result);
        }


        /// <summary>
        /// Update a menu category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuCategory"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(OperationId = "UpdateMenuCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the menu category.", typeof(MenuCategoryDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another menu category.", typeof(ErrorResultDto))]
        public async Task<ActionResult<MenuCategoryDto>> UpdateMenuCategoryAsync(Guid id, [FromBody] MenuCategoryUpdateDto menuCategory)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var category = await m_commandHandler.HandleCommandAsync<MenuCategoryUpdateCommand, MenuCategory>(
                new MenuCategoryUpdateCommand
                {
                    Id = id,
                    OrganizationId = validOrganization.OrganizationId,
                    Name = menuCategory.Name,
                    Weight = menuCategory.Weight,
                    Enabled = menuCategory.Enabled,
                });

            var result = m_mapper.Map<MenuCategory, MenuCategoryDto>(category);
            return Ok(result);
        }

        /// <summary>
        /// Delete a menu category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(OperationId = "DeleteMenuCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the deleted menu category.", typeof(MenuCategoryDto))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Returned when record was no longer present.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when the category is not empty.", typeof(ErrorResultDto))]
        public async Task<ActionResult<MenuCategoryDto>> DeleteMenuCategoryAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            
            var category = await m_commandHandler.HandleCommandAsync<MenuCategoryDeleteCommand, MenuCategory>(
                new MenuCategoryDeleteCommand
                {
                    OrganizationId = validOrganization.OrganizationId,
                    Id = id,
                });

            var result = category != null ? m_mapper.Map<MenuCategory, MenuCategoryDto>(category) : null;
            return Ok(result);
        }


    }
}
