using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO to update a product
    /// </summary>
    public sealed class ProductUpdateDto
    {
        /// <summary>
        /// Product category
        /// </summary>
        public Guid ProductCategoryId { get; set; }

        /// <summary>
        /// Dust category
        /// </summary>
        public Guid DustCategoryId { get; set; }

        /// <summary>
        /// Menu category
        /// </summary>
        public Guid MenuCategoryId { get; set; }

        /// <summary>
        /// Product code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Alternative name on menu
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// Vat indicator
        /// </summary>
        public VatLevel VatLevel { get; set; }

        /// <summary>
        /// Long description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Should be included in inventory
        /// </summary>
        public bool InInventory { get; set; }

        /// <summary>
        /// Amount of softdrugs for use in stock measurement
        /// </summary>
        public float Percentage { get; set; }

        /// <summary>
        /// Stock goes up on use (coffee machine counter)
        /// </summary>
        public bool AscendingStock { get; set; }

        /// <summary>
        /// Active/Inactive/Delete indicator
        /// </summary>
        public RecordState State { get; set; }

        /// <summary>
        /// Custom attributes
        /// </summary>
        public ProductCustomAttributeDto[] CustomAttributes { get; set; } = Array.Empty<ProductCustomAttributeDto>();

        /// <summary>
        /// Variations
        /// </summary>
        public ProductVariationUpdateDto[] Variations { get; set; } = Array.Empty<ProductVariationUpdateDto>();

        /// <summary>
        /// Selection pages
        /// </summary>
        public ProductSelectionPageReferenceDto[] SelectionPages { get; set; } = Array.Empty<ProductSelectionPageReferenceDto>();
    }
}
