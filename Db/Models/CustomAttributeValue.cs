namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class CustomAttributeValue
    {
        public Guid CustomAttributeValueId { get; set; }
        public Guid CustomAttributeId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }
        public CustomAttribute CustomAttribute { get; set; }
        public Product Product { get; set; }
    }
}
