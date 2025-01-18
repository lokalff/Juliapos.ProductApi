using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    /// <summary>
    /// Query to retrieve all product categories
    /// </summary>
    public sealed class ProductCategoriesQuery : IQuery<IEnumerable<ProductCategory>>
    {
        /// <summary>
        /// Organization id
        /// </summary>
        public Guid? OrganizationId { get; set; }
    }
}
