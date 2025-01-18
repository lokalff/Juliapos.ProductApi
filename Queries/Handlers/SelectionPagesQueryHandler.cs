using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="MenuCategoriesQuery"/>
    /// </summary>
    public sealed class SelectionPagesQueryHandler : IHandleQuery<SelectionPagesQuery, IEnumerable<SelectionPage>>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="SelectionPagesQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public SelectionPagesQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SelectionPage>> HandleAsync(SelectionPagesQuery query)
        {
            var q = m_dataStore.SelectionPageDataQuery.WithOrganization();

            if (query.OrganizationId != null)
                q = q.WhereOrganizationId(query.OrganizationId.Value);

            var result = await q.ToListAsync();
            return result;
        }
    }
}
