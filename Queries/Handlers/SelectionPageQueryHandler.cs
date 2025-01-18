using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    /// <summary>
    /// Handler for the <see cref="SelectionPageQuery"/>
    /// </summary>
    public sealed class SelectionPageQueryHandler : IHandleQuery<SelectionPageQuery, SelectionPage>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="MenuCategoryQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public SelectionPageQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<SelectionPage> HandleAsync(SelectionPageQuery query)
        {
            var result = await m_dataStore.SelectionPageDataQuery
                .WithOrganization()
                .WhereOrganizationId(query.OrganizationId)
                .WhereId(query.Id)
                .SingleOrDefaultAsync()
                ?? throw new HttpNotFoundException(ApiErrorCode.SelectionPageNotFound, query.Id); ;

            return result;
        }
    }
}
