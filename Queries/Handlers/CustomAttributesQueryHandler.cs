using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="CustomAttributesQuery"/>
    /// </summary>
    public sealed class CustomAttributesQueryHandler : IHandleQuery<CustomAttributesQuery, IEnumerable<CustomAttribute>>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="CustomAttributesQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public CustomAttributesQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CustomAttribute>> HandleAsync(CustomAttributesQuery query)
        {
            var q = m_dataStore.CustomAttributeDataQuery.WithOrganization();

            if (query.OrganizationId != null)
                q = q.WhereOrganizationId(query.OrganizationId.Value);

            var result = await q.ToListAsync();
            return result;
        }
    }
}
