using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    /// <summary>
    /// Query to retrieve all menu categories
    /// </summary>
    public sealed class MenuCategoriesQuery : IQuery<IEnumerable<MenuCategory>>
    {
        /// <summary>
        /// Organization id
        /// </summary>
        public Guid? OrganizationId { get; set; }
    }
}
