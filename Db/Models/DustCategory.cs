namespace Juliapos.Portal.ProductApi.Db.Models
{
    /// <summary>
    /// Dust category
    /// </summary>
    public sealed class DustCategory
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid DustCategoryId { get; set; }

        /// <summary>
        /// Organization id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Weight (order)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Owner organization
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// Products in this Category
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
