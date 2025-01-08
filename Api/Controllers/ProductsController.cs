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
        /// Get dayorder with id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(OperationId = "GetProductByIdAsync")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returned with the full information about the product.", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the product was not found.", typeof(ErrorResultDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returned when request can not be completed.", typeof(ErrorResultDto))]
        public async Task<ActionResult<ProductDto>> GetDayOrderByIdAsync(Guid id)
        {
            var validProduct = await m_argumentValidator.ValidateProductAsync(id);
            var existingProduct = await m_service.GetProductByIdAsync(validProduct.ProductId);

            var result = m_mapper.Map<Product, ProductDto>(existingProduct);
            return Ok(result);
        }


    }
}
