using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Db.DataQueries
{
    public interface IProductCategoryDataQuery : IDataQuery<ProductCategory>
    {
        IProductCategoryDataQuery WithOrganization();

        IProductCategoryDataQuery WhereId(Guid id);
        IProductCategoryDataQuery WhereOrganizationExternalId(Guid id);
        IProductCategoryDataQuery WhereOrganizationId(Guid id);
    }
}
