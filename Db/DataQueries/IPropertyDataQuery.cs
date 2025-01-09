using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IPropertyDataQuery : IDataQuery<Property>
    {
        IPropertyDataQuery WhereId(Guid id);
        IPropertyDataQuery WhereOrganizationExternalId(Guid id);
        IPropertyDataQuery WhereIdName(string idname, Guid organizationId);
        IPropertyDataQuery WhereIdNamesIn(IEnumerable<string> idname, Guid organizationId);
    }
}
