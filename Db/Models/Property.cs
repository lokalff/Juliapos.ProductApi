namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class Property
    {
        public Guid PropertyId { get; set; }
        public Guid OrganizationId { get; set; }
        public string IdName { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool Enabled { get; set; }

        public Organization Organization { get; set; }

        public ICollection<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();

    }
}
