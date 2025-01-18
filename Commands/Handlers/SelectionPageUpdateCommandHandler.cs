using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="SelectionPageUpdateCommand"/>
    /// </summary>
    public sealed class SelectionPageUpdateCommandHandler : IHandleCommand<SelectionPageUpdateCommand, SelectionPage>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="SelectionPageUpdateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public SelectionPageUpdateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<SelectionPage> HandleAsync(SelectionPageUpdateCommand command)
        {
            var existingPage = await m_dataStore.SelectionPageDataQuery
                .WhereId(command.Id)
                .WhereOrganizationId(command.OrganizationId)
                .SingleOrDefaultAsync() 
                ?? throw new HttpNotFoundException(ApiErrorCode.SelectionPageNotFound, command.Id);

            if (!command.Enabled && existingPage.Enabled)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .OnSelectionPage(existingPage.SelectionPageId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.SelectionPageHasProducts, existingPage.SelectionPageId);
            }


            existingPage.Name = command.Name;
            existingPage.Weight = command.Weight;
            existingPage.Enabled = command.Enabled;

            await m_dataStore.SaveChangesAsync();
            
            return existingPage;
        }
    }
}
