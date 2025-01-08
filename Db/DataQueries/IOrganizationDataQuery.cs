using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IOrganizationDataQuery : IDataQuery<Organization>
    {
        IOrganizationDataQuery WhereId(Guid id);
        IOrganizationDataQuery WhereExternalId(Guid id);
    }
}
