using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class SelectionPageDataQuery : DataQuery<IApiDbContext, SelectionPage>, ISelectionPageDataQuery
    {
        public SelectionPageDataQuery(IApiDbContext dataContext)
            : this(dataContext, dataContext.SelectionPage)
        {
        }

        private SelectionPageDataQuery(IApiDbContext dataContext, IQueryable<SelectionPage> queryable)
            : base(dataContext, queryable)
        {
        }

        public ISelectionPageDataQuery WithOrganization()
        {
            return new SelectionPageDataQuery(DataContext, Queryable.Include(c => c.Organization));
        }

        public ISelectionPageDataQuery WhereId(Guid id)
        {
            return new SelectionPageDataQuery(DataContext, Queryable.Where(c => c.SelectionPageId == id));
        }

        public ISelectionPageDataQuery WhereOrganizationExternalId(Guid id)
        {
            return new SelectionPageDataQuery(DataContext, Queryable
                .Include(c => c.Organization)
                .Where(c => c.Organization.ExternalId == id));
        }

        public ISelectionPageDataQuery WhereOrganizationId(Guid id)
        {
            return new SelectionPageDataQuery(DataContext, Queryable.Where(c => c.OrganizationId == id));
        }

    }
}
