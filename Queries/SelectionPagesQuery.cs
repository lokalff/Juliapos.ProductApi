using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    /// <summary>
    /// Query to retrieve all product selection pages
    /// </summary>
    public sealed class SelectionPagesQuery : IQuery<IEnumerable<SelectionPage>>
    {
        /// <summary>
        /// Organization id
        /// </summary>
        public Guid? OrganizationId { get; set; }
    }
}
