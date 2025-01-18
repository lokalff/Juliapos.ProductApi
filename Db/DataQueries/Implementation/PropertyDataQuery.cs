using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class PropertyDataQuery : DataQuery<IApiDbContext, Property>, IPropertyDataQuery
    {
        public PropertyDataQuery(IApiDbContext dataContext)
            : this(dataContext, dataContext.Property)
        {
        }

        private PropertyDataQuery(IApiDbContext dataContext, IQueryable<Property> queryable)
            : base(dataContext, queryable)
        {
        }

        public IPropertyDataQuery WithOrganization()
        {
            return new PropertyDataQuery(DataContext, Queryable.Include(c => c.Organization));
        }

        public IPropertyDataQuery WhereId(Guid id)
        {
            return new PropertyDataQuery(DataContext, Queryable.Where(c => c.PropertyId == id));
        }

        public IPropertyDataQuery WhereIdName(string idname, Guid organizationId)
        {
            return new PropertyDataQuery(DataContext, Queryable
                .Where(c => c.IdName == idname && c.OrganizationId == organizationId));
        }

        public IPropertyDataQuery WhereIdNamesIn(IEnumerable<string> idnames, Guid organizationId)
        {
            return new PropertyDataQuery(DataContext, Queryable
                .Where(c => idnames.Any(n => n == c.IdName && c.OrganizationId == organizationId)));
        }

        public IPropertyDataQuery WhereOrganizationExternalId(Guid id)
        {
            return new PropertyDataQuery(DataContext, Queryable
                .Include(c => c.Organization)
                .Where(c => c.Organization.ExternalId == id));
        }

        public IPropertyDataQuery WhereOrganizationId(Guid id)
        {
            return new PropertyDataQuery(DataContext, Queryable.Where(c => c.OrganizationId == id));
        }
    }
}
