using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="PropertiesQuery"/>
    /// </summary>
    public sealed class PropertiesQueryHandler : IHandleQuery<PropertiesQuery, IEnumerable<Property>>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="PropertiesQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public PropertiesQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Property>> HandleAsync(PropertiesQuery query)
        {
            var q = m_dataStore.PropertyDataQuery.WithOrganization();

            if (query.OrganizationId != null)
                q = q.WhereOrganizationId(query.OrganizationId.Value);

            var result = await q.ToListAsync();
            return result;
        }
    }
}
