using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface ISelectionPageDataQuery : IDataQuery<SelectionPage>
    {
        ISelectionPageDataQuery WhereId(Guid id);
        ISelectionPageDataQuery WhereOrganizationExternalId(Guid id);
    }
}
