using Juliapos.Patterns.DataAccess;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IApiDbDataStore : IDataStore
    {
        IProductCategoryDataQuery ProductCategoryDataQuery { get; }
        IDustCategoryDataQuery DustCategoryDataQuery { get; }
        IMenuCategoryDataQuery MenuCategoryDataQuery { get; }
        ISelectionPageDataQuery SelectionPageDataQuery { get; }
        IProductDataQuery ProductDataQuery { get; }
        IOrganizationDataQuery OrganizationDataQuery { get; }
        ILocationDataQuery LocationDataQuery { get; }
        ICustomAttributeDataQuery CustomAttributeDataQuery { get; }

        //IQueryable<PackagesAtResult> GetPackagesAt(DateTime at, Guid? OrganizationId, Guid? locationId);
    }
}
