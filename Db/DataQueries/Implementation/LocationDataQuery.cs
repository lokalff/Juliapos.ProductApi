using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class LocationDataQuery : DataQuery<IApiDbContext, Location>, ILocationDataQuery
    {
        public LocationDataQuery(IApiDbContext dataContext)
            : this(dataContext, dataContext.Location)
        {
        }

        private LocationDataQuery(IApiDbContext dataContext, IQueryable<Location> queryable)
            : base(dataContext, queryable)
        {
        }

        public ILocationDataQuery WhereId(Guid id)
        {
            return new LocationDataQuery(DataContext, Queryable.Where(c => c.LocationId == id));
        }

        public ILocationDataQuery WhereOrganizationId(Guid id)
        {
            return new LocationDataQuery(DataContext, Queryable.Where(c => c.OrganizationId == id));
        }

        public ILocationDataQuery WhereOrganizationExternalId(Guid id)
        {
            return new LocationDataQuery(DataContext, Queryable
                .Include(c => c.Organization)
                .Where(c => c.Organization.ExternalId == id));
        }

    }
}
