using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IMenuCategoryDataQuery : IDataQuery<MenuCategory>
    {
        IMenuCategoryDataQuery WithOrganization();
        IMenuCategoryDataQuery WhereId(Guid id);
        IMenuCategoryDataQuery WhereOrganizationExternalId(Guid id);
        IMenuCategoryDataQuery WhereOrganizationId(Guid id);
    }
}
