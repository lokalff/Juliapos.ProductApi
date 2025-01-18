using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ProductDto
    {
        public Guid Id { get; set; }
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

        public DateTime Created { get; set; }
        public string UserCreate { get; set; }
        public DateTime? Updated { get; set; }
        public string UserUpdate { get; set; }

        public ProductPropertyDto[] Properties { get; set; } = Array.Empty<ProductPropertyDto>();
        public ProductVariationAddDto[] Variations { get; set; } = Array.Empty<ProductVariationAddDto>();
    }
}
