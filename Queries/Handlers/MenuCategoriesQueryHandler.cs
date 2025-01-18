using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="MenuCategoriesQuery"/>
    /// </summary>
    public sealed class MenuCategoriesQueryHandler : IHandleQuery<MenuCategoriesQuery, IEnumerable<MenuCategory>>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="MenuCategoriesQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public MenuCategoriesQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MenuCategory>> HandleAsync(MenuCategoriesQuery query)
        {
            var q = m_dataStore.MenuCategoryDataQuery.WithOrganization();

            if (query.OrganizationId != null)
                q = q.WhereOrganizationId(query.OrganizationId.Value);

            var result = await q.ToListAsync();
            return result;
        }
    }
}
