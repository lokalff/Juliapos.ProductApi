namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class MenuCategory
    {
        public Guid OrganizationId { get; set; }
        public Guid MenuCategoryId { get; set; }
        public string IdName { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public bool Enabled { get; set; }

        public Organization Organization { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
