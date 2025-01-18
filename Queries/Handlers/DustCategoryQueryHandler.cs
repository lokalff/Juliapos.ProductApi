using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Queries.Handlers
{
    public sealed class DustCategoryQueryHandler : IHandleQuery<DustCategoryQuery, DustCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="DustCategoryQueryHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public DustCategoryQueryHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<DustCategory> HandleAsync(DustCategoryQuery query)
        {
            var result = await m_dataStore.DustCategoryDataQuery
                .WithOrganization()
                .WhereOrganizationId(query.OrganizationId)
                .WhereId(query.Id)
                .SingleOrDefaultAsync()
                ?? throw new HttpNotFoundException(ApiErrorCode.DustCategoryNotFound, query.Id); ;

            return result;
        }
    }
}
