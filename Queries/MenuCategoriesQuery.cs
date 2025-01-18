using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    public sealed class MenuCategoriesQuery : IQuery<IEnumerable<MenuCategory>>
    {
        public Guid? OrganizationId { get; set; }
    }
}
