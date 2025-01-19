using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class CustomAttributeDataQuery : DataQuery<IApiDbContext, CustomAttribute>, ICustomAttributeDataQuery
    {
        public CustomAttributeDataQuery(IApiDbContext dataContext)
            : this(dataContext, dataContext.CustomAttribute)
        {
        }

        private CustomAttributeDataQuery(IApiDbContext dataContext, IQueryable<CustomAttribute> queryable)
            : base(dataContext, queryable)
        {
        }

        public ICustomAttributeDataQuery WithOrganization()
        {
            return new CustomAttributeDataQuery(DataContext, Queryable.Include(c => c.Organization));
        }

        public ICustomAttributeDataQuery WhereId(Guid id)
        {
            return new CustomAttributeDataQuery(DataContext, Queryable.Where(c => c.CustomAttributeId == id));
        }

        public ICustomAttributeDataQuery WhereIdName(string idname, Guid organizationId)
        {
            return new CustomAttributeDataQuery(DataContext, Queryable
                .Where(c => c.IdName == idname && c.OrganizationId == organizationId));
        }

        public ICustomAttributeDataQuery WhereIdNamesIn(IEnumerable<string> idnames, Guid organizationId)
        {
            return new CustomAttributeDataQuery(DataContext, Queryable
                .Where(c => idnames.Any(n => n == c.IdName && c.OrganizationId == organizationId)));
        }

        public ICustomAttributeDataQuery WhereOrganizationExternalId(Guid id)
        {
            return new CustomAttributeDataQuery(DataContext, Queryable
                .Include(c => c.Organization)
                .Where(c => c.Organization.ExternalId == id));
        }

        public ICustomAttributeDataQuery WhereOrganizationId(Guid id)
        {
            return new CustomAttributeDataQuery(DataContext, Queryable.Where(c => c.OrganizationId == id));
        }
    }
}
