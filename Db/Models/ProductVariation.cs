namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class ProductVariation
    {
        public Guid ProductVariationId { get; set; }
        public Guid ProductId { get; set; }

        public string Sku { get; set; }
        public string Code { get; set; }      // added to code of product
        public string Name { get; set; }      // "5 gr" of "3 gr"

        public Product Product { get; set; }
        public ICollection<ProductVariationLocation> ProductVariationLocations { get; set; }
    }
}
