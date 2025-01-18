using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    public sealed class DustCategoriesQuery : IQuery<IEnumerable<DustCategory>>
    {
        public Guid? OrganizationId { get; set; }
    }
}
