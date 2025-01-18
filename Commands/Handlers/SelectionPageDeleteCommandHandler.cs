using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="SelectionPageDeleteCommand"/>
    /// </summary>
    public sealed class SelectionPageDeleteCommandHandler : IHandleCommand<SelectionPageDeleteCommand, SelectionPage>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="SelectionPageDeleteCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public SelectionPageDeleteCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<SelectionPage> HandleAsync(SelectionPageDeleteCommand command)
        {
            var existingPage = await m_dataStore.SelectionPageDataQuery
                .WhereOrganizationId(command.OrganizationId)
                .WhereId(command.Id)
                .SingleOrDefaultAsync();

            if (existingPage != null)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .OnSelectionPage(command.Id)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.SelectionPageHasProducts, command.Id);

                m_dataStore.Remove(existingPage);
                await m_dataStore.SaveChangesAsync();
            }

            return existingPage;
        }
    }
}
