using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="CustomAttributeQuery"/>
    /// </summary>
    public sealed class CustomAttributeQueryHandler : IHandleQuery<CustomAttributeQuery, CustomAttribute>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="CustomAttributeQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public CustomAttributeQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<CustomAttribute> HandleAsync(CustomAttributeQuery query)
        {
            var result = await m_dataStore.CustomAttributeDataQuery
                .WithOrganization()
                .WhereOrganizationId(query.OrganizationId)
                .WhereId(query.Id)
                .SingleOrDefaultAsync()
                ?? throw new HttpNotFoundException(ApiErrorCode.CustomAttributeNotFound, query.Id); ;

            return result;
        }
    }
}
