namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class SelectionPage
    {
        public Guid OrganizationId { get; set; }
        public Guid SelectionPageId { get; set; }
        public string IdName { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public bool Enabled { get; set; }

        public Organization Organization { get; set; }
        public ICollection<SelectionPageProduct> SelectionPageProducts { get; set; } = new List<SelectionPageProduct>();
    }
}
