using Juliapos.Patterns.DataAccess;
using Juliapos.Portal.ProductApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    internal sealed class MenuCategoryDataQuery : DataQuery<IApiDbContext, MenuCategory>, IMenuCategoryDataQuery
    {
        public MenuCategoryDataQuery(IApiDbContext dataContext)
            : this(dataContext, dataContext.MenuCategory)
        {
        }

        private MenuCategoryDataQuery(IApiDbContext dataContext, IQueryable<MenuCategory> queryable)
            : base(dataContext, queryable)
        {
        }

        public IMenuCategoryDataQuery WhereId(Guid id)
        {
            return new MenuCategoryDataQuery(DataContext, Queryable.Where(c => c.MenuCategoryId == id));
        }

        public IMenuCategoryDataQuery WhereOrganizationExternalId(Guid id)
        {
            return new MenuCategoryDataQuery(DataContext, Queryable
                .Include(c => c.Organization)
                .Where(c => c.Organization.ExternalId == id));
        }

    }
}
