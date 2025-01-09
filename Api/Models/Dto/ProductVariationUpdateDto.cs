namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    public sealed class ProductVariationUpdateDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; }
        public string Code { get; set; }      // added to code of product
        public string Name { get; set; }      // "5 gr" of "3 gr"

        public ProductVariationLocationReferenceDto[] ProductVariationLocations { get; set; }
    }
}
