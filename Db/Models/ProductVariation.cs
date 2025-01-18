namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class ProductVariation
    {
        public Guid ProductVariationId { get; set; }
        public Guid ProductId { get; set; }

        public string Sku { get; set; }
        public string CodeExtension { get; set; }      // added to code of product
        public string NameExtension { get; set; }      // "5 gr" of "3 gr"

        public Product Product { get; set; }
        public ICollection<ProductVariationLocation> ProductVariationLocations { get; set; }
    }
}
