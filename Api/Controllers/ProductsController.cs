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
    /// Handle all product related requests
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IEndpointArgumentValidator m_argumentValidator;
        private readonly IProductsService m_service;
        private readonly IDtoMapper m_mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="mapper"></param>
        /// <param name="endpointArgumentValidator"></param>
        public ProductsController(
            IProductsService service,
            IDtoMapper mapper,
            IEndpointArgumentValidator endpointArgumentValidator)
        {
            m_service = service;
            m_mapper = mapper;
            m_argumentValidator = endpointArgumentValidator;
        }

        /// <summary>
        /// Get products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetProductsAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product.", typeof(IEnumerable<ProductDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync()
        {
            var validOrganization = await m_argumentValidator.ValidateCurrentOrganizationAsync();
            var products = await m_service.GetProductsAsync(validOrganization.OrganizationId);
            var result = m_mapper.Map<Product, ProductDto>(products);
            return Ok(result);
        }

        /// <summary>
        /// Get product with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(OperationId = "GetProductByIdAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product.", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the product was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(Guid id)
        {
            var validProduct = await m_argumentValidator.ValidateProductAsync(id);
            var existingProduct = await m_service.GetProductByIdAsync(validProduct.ProductId);

            var result = m_mapper.Map<Product, ProductDto>(existingProduct);
            return Ok(result);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateProductAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the new product.", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another product.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] ProductAddDto product)
        {
            var id = Guid.NewGuid();

            var validProductCategory = await m_argumentValidator.ValidateProductCategoryAsync(product.ProductCategoryId);
            var validMenuCategory = await m_argumentValidator.ValidateMenuCategoryAsync(product.MenuCategoryId);
            var validDustCategory = await m_argumentValidator.ValidateDustCategoryAsync(product.DustCategoryId);

            product.Properties.Select(async s => await m_argumentValidator.ValidatePropertyAsync(s.Id));
            product.SelectionPages.Select(async s => await m_argumentValidator.ValidateSelectionPageAsync(s.SelectionPageId));
            product.Variations.SelectMany(v => v.ProductVariationLocations).Select(async vl =>
            {
                if (vl.LocationId != null)
                    await m_argumentValidator.ValidateLocationAsync(vl.LocationId.Value);
            });

            var productToAdd = product.MapProductAdd(id);
            productToAdd = await m_service.CreateProductAsync(productToAdd);

            var result = m_mapper.Map<Product, ProductDto>(productToAdd);
            return Ok(result);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(OperationId = "UpdateProductAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product.", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Returned when there is a conflict with another product.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductDto>> UpdateProductAsync(Guid id, [FromBody] ProductUpdateDto product)
        {
            var validProduct = await m_argumentValidator.ValidateProductAsync(id);

            var validProductCategory = await m_argumentValidator.ValidateProductCategoryAsync(product.ProductCategoryId);
            var validMenuCategory = await m_argumentValidator.ValidateMenuCategoryAsync(product.MenuCategoryId);
            var validDustCategory = await m_argumentValidator.ValidateDustCategoryAsync(product.DustCategoryId);

            product.Properties.Select(async s => await m_argumentValidator.ValidatePropertyAsync(s.Id));
            product.SelectionPages.Select(async s => await m_argumentValidator.ValidateSelectionPageAsync(s.SelectionPageId));
            product.Variations.SelectMany(v => v.ProductVariationLocations).Select(async vl =>
            {
                if (vl.LocationId != null)
                    await m_argumentValidator.ValidateLocationAsync(vl.LocationId.Value);
            });

            var productToUpdate = product.MapProductUpdate(id);
            productToUpdate = await m_service.UpdateProductAsync(productToUpdate);

            var result = m_mapper.Map<Product, ProductDto>(productToUpdate);
            return Ok(result);
        }

        /// <summary>
        /// Delete a prouct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="purgeProduct"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(OperationId = "DeleteProductAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the deleted product.", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Returned when record was no longer present.")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Returned when the location is not empty.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductDto>> DeleteProductAsync(Guid id, bool purgeProduct)
        {
            var validProduct = await m_argumentValidator.ValidateProductAsync(id);
            var product = await m_service.DeleteProductAsync(id, purgeProduct);

            var result = m_mapper.Map<Product, ProductDto>(product);
            return Ok(result);
        }
    }
}
