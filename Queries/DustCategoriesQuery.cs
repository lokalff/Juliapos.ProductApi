using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    /// <summary>
    /// Query to retrieve all dust categories
    /// </summary>
    public sealed class DustCategoriesQuery : IQuery<IEnumerable<DustCategory>>
    {
        /// <summary>
        /// Organization id
        /// </summary>
        public Guid? OrganizationId { get; set; }
    }
}
