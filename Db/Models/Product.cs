namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class Product
    {
        public Guid ProductId { get; set; }
        public Guid ProductCategoryId { get; set; }
        public Guid DustCategoryId { get; set; }
        public Guid MenuCategoryId { get; set; }

        //public string Code { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
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

        public DateTime? Deleted { get; set; }
        public string UserDelete { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public MenuCategory MenuCategory { get; set; }
        public DustCategory DustCategory { get; set; }
        public ICollection<PropertyValue> PropertieValues { get; set; } = new List<PropertyValue>();
        public ICollection<SelectionPageProduct> SelectionPageProducts { get; set; } = new List<SelectionPageProduct>();
        public ICollection<ProductVariation> ProductVariations { get; set; } = new List<ProductVariation>();

        // Start end ?
    }
}
