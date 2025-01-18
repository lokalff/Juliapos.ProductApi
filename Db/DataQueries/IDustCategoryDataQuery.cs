using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IDustCategoryDataQuery : IDataQuery<DustCategory>
    {
        IDustCategoryDataQuery WithOrganization();
        IDustCategoryDataQuery WhereId(Guid id);
        IDustCategoryDataQuery WhereOrganizationExternalId(Guid id);
        IDustCategoryDataQuery WhereOrganizationId(Guid id);
    }
}
