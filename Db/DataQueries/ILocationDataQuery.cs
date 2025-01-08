using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface ILocationDataQuery : IDataQuery<Location>
    {
        ILocationDataQuery WhereId(Guid id);
        ILocationDataQuery WhereOrganizationExternalId(Guid id);
        ILocationDataQuery WhereOrganizationId(Guid id);
    }
}
