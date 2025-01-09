namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class Organization
    {
        public Guid OrganizationId { get; set; }
        public Guid? ExternalId { get; set; }
        public string Name { get; set; }

        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public ICollection<DustCategory> DustCategories { get; set; } = new List<DustCategory>();
        public ICollection<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
