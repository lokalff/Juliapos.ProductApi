namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class ProductCategory
    {
        public Guid OrganizationId { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string IdName { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public MeasureMethod MeasureMethod { get; set; }           // None, Count, Weigh
        public string DefaultForeColor { get; set; }
        public string DefaultBackColor { get; set; }
        public bool Enabled { get; set; }

        public Organization Organization { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
