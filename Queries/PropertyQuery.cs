using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    /// <summary>
    /// Query for one property
    /// </summary>
    public sealed class PropertyQuery : IQuery<Property>
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
