using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IProductDataQuery : IDataQuery<Product>
    {
        IProductDataQuery WithLocations();
        IProductDataQuery WithProperties();
        IProductDataQuery WithVariations();
        IProductDataQuery WhereId(Guid id);
        IProductDataQuery WhereProductCategoryId(Guid id);
        IProductDataQuery WhereDustCategoryId(Guid id);
        IProductDataQuery WhereMenuCategoryId(Guid id);
        IProductDataQuery WhereNotDeleted();
        IProductDataQuery WhereOrganizationId(Guid id);
        IProductDataQuery WhereOrganizationExternalId(Guid id);

        IProductDataQuery HasProperty(Guid id);
    }
}
