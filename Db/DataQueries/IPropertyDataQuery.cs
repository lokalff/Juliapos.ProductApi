using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IPropertyDataQuery : IDataQuery<Property>
    {
        /// <summary>
        /// Include the organization of this property
        /// </summary>
        /// <returns></returns>
        IPropertyDataQuery WithOrganization();

        IPropertyDataQuery WhereId(Guid id);
        IPropertyDataQuery WhereOrganizationExternalId(Guid id);
        IPropertyDataQuery WhereOrganizationId(Guid id);
        IPropertyDataQuery WhereIdName(string idname, Guid organizationId);
        IPropertyDataQuery WhereIdNamesIn(IEnumerable<string> idname, Guid organizationId);
    }
}
