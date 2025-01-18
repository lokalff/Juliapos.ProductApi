using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    /// <summary>
    /// Query to retrieve a product category
    /// </summary>
    public sealed class ProductCategoryQuery : IQuery<ProductCategory>
    {
        /// <summary>
        /// Organization id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
    }
}
