namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class DustCategory
    {
        public Guid OrganizationId { get; set; }
        public Guid DustCategoryId { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
