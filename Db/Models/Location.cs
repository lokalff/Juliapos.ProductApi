namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class Location
    {
        public Guid OrganizationId { get; set; }
        public Guid LocationId { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }

        public Organization Organization { get; set; }
        public ICollection<ProductVariationLocation> ProductVariationLocations { get; set; }
    }
}
