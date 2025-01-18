namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class MenuCategory
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid MenuCategoryId { get; set; }

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
        /// Id as a name
        /// </summary>
        public string IdName { get; set; }

        /// <summary>
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; }

        public Organization Organization { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
