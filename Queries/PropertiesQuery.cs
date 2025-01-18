using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    /// <summary>
    /// Query to retrieve all properties
    /// </summary>
    public sealed class PropertiesQuery : IQuery<IEnumerable<Property>>
    {
        /// <summary>
        /// Organization id
        /// </summary>
        public Guid? OrganizationId { get; set; }
    }
}
