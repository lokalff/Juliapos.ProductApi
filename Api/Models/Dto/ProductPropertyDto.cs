namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// Property instance in a product
    /// </summary>
    public sealed class ProductPropertyDto
    {
        /// <summary>
        /// Property id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Property value
        /// </summary>
        public string Value { get; set; }
    }
}
