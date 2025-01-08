namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class ProductVariationLocation
    {
        public Guid ProductVariationLocationId { get; set; }
        public Guid ProductVariationId { get; set; }
        public Guid LocationId { get; set; }

        public float? UnitPrice { get; set; }
        public float? UnitPricePurchase { get; set; }
        public bool? ShowOnFavoritePage { get; set; }
        public float? MinAmount { get; set; }
        public float? MaxAmount { get; set; }

        public float? Transport { get; set; }

        public ProductStatus Status { get; set; }
        public ProductStatus? NextStatus { get; set; }
        public DateTime? ChangeDateTime { get; set; }

        public TimeOnly? OnMenuStart { get; set; }
        public TimeOnly? OnMenuEnd { get; set; }

        public ProductVariation ProductVariation { get; set; }
        public Location Location { get; set; }
    }
}
