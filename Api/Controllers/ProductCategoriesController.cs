using Juliapos.Patterns.DtoMapping;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;
using Juliapos.Portal.ProductApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Juliapos.Portal.ProductApi.Api.Controllers
{
    /// <summary>
    /// Handle all product category related requests
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public sealed class ProductCategoriesController : ControllerBase
    {
        private readonly IEndpointArgumentValidator m_argumentValidator;
        private readonly IProductCategoriesService m_service;
        private readonly IDtoMapper m_mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="mapper"></param>
        /// <param name="endpointArgumentValidator"></param>
        public ProductCategoriesController(
            IProductCategoriesService service,
            IDtoMapper mapper,
            IEndpointArgumentValidator endpointArgumentValidator)
        {
            m_service = service;
            m_mapper = mapper;
            m_argumentValidator = endpointArgumentValidator;
        }

        /// <summary>
        /// Get product categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetProductCategoriesAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product category.", typeof(IEnumerable<ProductCategoryDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategoriesAsync()
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var products = await m_service.GetProductCategoriesAsync(validOrganization.OrganizationId);
            var result = m_mapper.Map<ProductCategory, ProductCategoryDto>(products);
            return Ok(result);
        }

        /// <summary>
        /// Get product category with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(OperationId = "GetProductCategoryByIdAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product category.", typeof(ProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the product category was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductDto>> GetProductCategoryByIdAsync(Guid id)
        {
            var validProductCategory = await m_argumentValidator.ValidateProductCategoryAsync(id);
            var existingProductCategory = await m_service.GetProductCategoryByIdAsync(validProductCategory.ProductCategoryId);

            var result = m_mapper.Map<ProductCategory, ProductCategoryDto>(existingProductCategory);
            return Ok(result);
        }

        /// <summary>
        /// Create a new product category
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateProductCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the new product category.", typeof(ProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another product category.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductCategoryDto>> CreateProductCategoryAsync([FromBody] ProductCategoryAddDto productCategory)
        {
            var id = Guid.NewGuid();
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();

            var categoryToAdd = new ProductCategory
            {
                OrganizationId = validOrganization.OrganizationId,
                ProductCategoryId = id,
                IdName = productCategory.IdName,
                Name = productCategory.Name,
                Weight = productCategory.Weight,
                MeasureMethod = productCategory.MeasureMethod,
                DefaultForeColor = productCategory.DefaultForeColor,
                DefaultBackColor = productCategory.DefaultBackColor,
                Enabled = productCategory.Enabled,
            };
            categoryToAdd = await m_service.CreateProductCategoryAsync(categoryToAdd);
            var result = m_mapper.Map<ProductCategory, ProductCategoryDto>(categoryToAdd);
            return Ok(result);
        }


        /// <summary>
        /// Update a product category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(OperationId = "UpdateProductCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product category.", typeof(ProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another product category.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductCategoryDto>> UpdateProductCategoryAsync(Guid id, [FromBody] ProductCategoryUpdateDto productCategory)
        {
            var validProductCategory = await m_argumentValidator.ValidateProductCategoryAsync(id);

            var categoryToUpdate = new ProductCategory
            {
                ProductCategoryId = id,
                Name = productCategory.Name,
                Weight = productCategory.Weight,
                MeasureMethod = productCategory.MeasureMethod,
                DefaultForeColor = productCategory.DefaultForeColor,
                DefaultBackColor = productCategory.DefaultBackColor,
                Enabled = productCategory.Enabled,
            };
            categoryToUpdate = await m_service.UpdateProductCategoryAsync(categoryToUpdate);

            var result = m_mapper.Map<ProductCategory, ProductCategoryDto>(categoryToUpdate);
            return Ok(result);
        }

        /// <summary>
        /// Delete a product category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(OperationId = "DeleteProductCategoryAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the deleted product category.", typeof(ProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Returned when record was no longer present.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when the category is not empty.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductDto>> DeleteProductAsync(Guid id)
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var productCategory = await m_service.DeleteProductCategoryAsync(id, validOrganization.OrganizationId);

            var result = productCategory != null ? m_mapper.Map<ProductCategory, ProductCategoryDto>(productCategory) : null;
            return Ok(result);
        }


    }
}
