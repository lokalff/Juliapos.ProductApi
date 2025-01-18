using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="PropertyQuery"/>
    /// </summary>
    public sealed class PropertyQueryHandler : IHandleQuery<PropertyQuery, Property>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="PropertyQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public PropertyQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<Property> HandleAsync(PropertyQuery query)
        {
            var result = await m_dataStore.PropertyDataQuery
                .WithOrganization()
                .WhereOrganizationId(query.OrganizationId)
                .WhereId(query.Id)
                .SingleOrDefaultAsync()
                ?? throw new HttpNotFoundException(ApiErrorCode.PropertyNotFound, query.Id); ;

            return result;
        }
    }
}
