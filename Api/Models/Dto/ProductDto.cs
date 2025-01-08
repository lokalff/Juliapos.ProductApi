using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ProductDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string MenuName { get; set; }
        public VatLevel VatLevel { get; set; }
        public string Description { get; set; }
        public bool InInventory { get; set; }
        public float Percentage { get; set; }
        public bool AscendingStock { get; set; }
        public RecordState State { get; set; }

        public DateTime Created { get; set; }
        public string UserCreate { get; set; }
        public DateTime Updated { get; set; }
        public string UserUpdate { get; set; }

        public PropertyReferenceDto[] Properties { get; set; } = Array.Empty<PropertyReferenceDto>();
        public ProductVariationReferenceDto[] Variations { get; set; } = Array.Empty<ProductVariationReferenceDto>();
    }
}
