using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface ICustomAttributeDataQuery : IDataQuery<CustomAttribute>
    {
        /// <summary>
        /// Include the organization of the custom attribute
        /// </summary>
        /// <returns></returns>
        ICustomAttributeDataQuery WithOrganization();

        ICustomAttributeDataQuery WhereId(Guid id);
        ICustomAttributeDataQuery WhereOrganizationExternalId(Guid id);
        ICustomAttributeDataQuery WhereOrganizationId(Guid id);
        ICustomAttributeDataQuery WhereIdName(string idname, Guid organizationId);
        ICustomAttributeDataQuery WhereIdNamesIn(IEnumerable<string> idname, Guid organizationId);
    }
}
