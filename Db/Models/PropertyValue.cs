namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class PropertyValue
    {
        public Guid PropertyValueId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }
        public Property Property { get; set; }
        public Product Product { get; set; }
    }
}
