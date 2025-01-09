using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO to add a product
    /// </summary>
    public sealed class ProductAddDto
    {
        public Guid ProductCategoryId { get; set; }
        public Guid DustCategoryId { get; set; }
        public Guid MenuCategoryId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string MenuName { get; set; }
        public VatLevel VatLevel { get; set; }
        public string Description { get; set; }
        public bool InInventory { get; set; }
        public float Percentage { get; set; }
        public bool AscendingStock { get; set; }
        public RecordState State { get; set; }

        public PropertyReferenceDto[] Properties { get; set; } = Array.Empty<PropertyReferenceDto>();
        public ProductVariationAddDto[] Variations { get; set; } = Array.Empty<ProductVariationAddDto>();
        public ProductSelectionPageReferenceDto[] SelectionPages { get; set; } = Array.Empty<ProductSelectionPageReferenceDto>();

    }
}
