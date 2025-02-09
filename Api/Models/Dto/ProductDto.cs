using System.ComponentModel.DataAnnotations;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for products
    /// </summary>
    public sealed class ProductDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Product category
        /// </summary>
        [Required]
        public Guid ProductCategoryId { get; set; }

        /// <summary>
        /// Dust category
        /// </summary>
        [Required]
        public Guid DustCategoryId { get; set; }

        /// <summary>
        /// Menu category
        /// </summary>
        [Required]
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
        [Required]
        public VatLevel VatLevel { get; set; }

        /// <summary>
        /// Long description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Should be included in inventory
        /// </summary>
        [Required]
        public bool InInventory { get; set; }

        /// <summary>
        /// Amount of softdrugs for use in stock measurement
        /// </summary>
        [Required]
        public float Percentage { get; set; }

        /// <summary>
        /// Stock goes up on use (coffee machine counter)
        /// </summary>
        [Required]
        public bool AscendingStock { get; set; }

        /// <summary>
        /// Active/Inactive/Delete indicator
        /// </summary>
        [Required]
        public RecordState State { get; set; }

        /// <summary>
        /// Datetime created
        /// </summary>
        [Required]
        public DateTime Created { get; set; }

        /// <summary>
        /// User that created the product
        /// </summary>
        public string UserCreate { get; set; }

        /// <summary>
        /// Datetime last update
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// User last update
        /// </summary>
        public string UserUpdate { get; set; }

        /// <summary>
        /// Custom attributes
        /// </summary>
        public ProductCustomAttributeDto[] CustomAttributes { get; set; } = Array.Empty<ProductCustomAttributeDto>();

        /// <summary>
        /// Variations
        /// </summary>
        public ProductVariationDto[] Variations { get; set; } = Array.Empty<ProductVariationDto>();
    }
}
