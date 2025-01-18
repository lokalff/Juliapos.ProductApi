using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    public sealed class DustCategoriesQueryHandler : IHandleQuery<DustCategoriesQuery, IEnumerable<DustCategory>>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="DustCategoriesQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public DustCategoriesQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DustCategory>> HandleAsync(DustCategoriesQuery query)
        {
            var q = m_dataStore.DustCategoryDataQuery.WithOrganization();

            if (query.OrganizationId != null)
                q = q.WhereOrganizationId(query.OrganizationId.Value);

            var result = await q.ToListAsync();
            return result;
        }
    }
}
