namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// Custom attribute instance in a product
    /// </summary>
    public sealed class ProductCustomAttributeDto
    {
        /// <summary>
        /// Custom attribute id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Custom attribute value
        /// </summary>
        public string Value { get; set; }
    }
}
