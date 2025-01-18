using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO to update a product
    /// </summary>
    public sealed class ProductUpdateDto
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

        public ProductPropertyDto[] Properties { get; set; } = Array.Empty<ProductPropertyDto>();
        public ProductVariationUpdateDto[] Variations { get; set; } = Array.Empty<ProductVariationUpdateDto>();
        public ProductSelectionPageReferenceDto[] SelectionPages { get; set; } = Array.Empty<ProductSelectionPageReferenceDto>();

    }
}
