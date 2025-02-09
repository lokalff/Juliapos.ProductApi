namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for product variation
    /// </summary>
    public sealed class ProductVariationDto
    {
        /// <summary>
        /// Globally unique product code
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Locally unique product code
        /// </summary>
        public string Code { get; set; }      // added to code of product

        /// <summary>
        /// Variation name 
        /// </summary>
        public string Name { get; set; }      // "5 gr" of "3 gr"

        /// <summary>
        /// Product Location values
        /// </summary>
        public ProductVariationLocationDto[] ProductVariationLocations { get; set; }
    }
}
