using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    public sealed class ProductVariationLocationDto
    {
        public Guid? LocationId { get; set; }

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
    }
}
