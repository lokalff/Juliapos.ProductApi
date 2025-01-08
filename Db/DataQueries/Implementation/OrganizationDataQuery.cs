using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class OrganizationDataQuery : DataQuery<IApiDbContext, Organization>, IOrganizationDataQuery
    {
        public OrganizationDataQuery(IApiDbContext dataContext) 
            : this(dataContext, dataContext.Organization)
        {
        }

        private OrganizationDataQuery(IApiDbContext dataContext, IQueryable<Organization> queryable)
            : base(dataContext, queryable)
        {
        }

        public IOrganizationDataQuery WithLocation()
        {
            return new OrganizationDataQuery(DataContext, Queryable.Include(c => c.Locations));
        }

        public IOrganizationDataQuery WhereId(Guid id)
        {
            return new OrganizationDataQuery(DataContext, Queryable.Where(c => c.OrganizationId == id));
        }

        public IOrganizationDataQuery WhereExternalId(Guid id)
        {
            return new OrganizationDataQuery(DataContext, Queryable.Where(c => c.ExternalId == id));
        }
    }
}
