using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    public sealed class MenuCategoryQueryHandler : IHandleQuery<MenuCategoryQuery, MenuCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="MenuCategoryQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public MenuCategoryQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<MenuCategory> HandleAsync(MenuCategoryQuery query)
        {
            var result = await m_dataStore.MenuCategoryDataQuery
                .WithOrganization()
                .WhereOrganizationId(query.OrganizationId)
                .WhereId(query.Id)
                .SingleOrDefaultAsync()
                ?? throw new HttpNotFoundException(ApiErrorCode.MenuCategoryNotFound, query.Id); ;

            return result;
        }
    }
}
