using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class DustCategoryDataQuery : DataQuery<IApiDbContext, DustCategory>, IDustCategoryDataQuery
    {
        public DustCategoryDataQuery(IApiDbContext dataContext)
            : this(dataContext, dataContext.DustCategory)
        {
        }

        private DustCategoryDataQuery(IApiDbContext dataContext, IQueryable<DustCategory> queryable)
            : base(dataContext, queryable)
        {
        }

        public IDustCategoryDataQuery WhereId(Guid id)
        {
            return new DustCategoryDataQuery(DataContext, Queryable.Where(c => c.DustCategoryId == id));
        }

        public IDustCategoryDataQuery WhereOrganizationExternalId(Guid id)
        {
            return new DustCategoryDataQuery(DataContext, Queryable
                .Include(c => c.Organization)
                .Where(c => c.Organization.ExternalId == id));
        }

    }
}
