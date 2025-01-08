using Juliapos.Patterns.DataAccess;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IApiDbDataStore : IDataStore
    {
        IProductDataQuery ProductDataQuery { get; }
        IOrganizationDataQuery OrganizationDataQuery { get; }
        ILocationDataQuery LocationDataQuery { get; }

        //IQueryable<PackagesAtResult> GetPackagesAt(DateTime at, Guid? OrganizationId, Guid? locationId);
    }
}
