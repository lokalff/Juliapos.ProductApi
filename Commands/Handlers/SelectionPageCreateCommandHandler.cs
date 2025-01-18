using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="SelectionPageCreateCommand"/>
    /// </summary>
    public sealed class SelectionPageCreateCommandHandler : IHandleCommand<SelectionPageCreateCommand, SelectionPage>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="SelectionPageCreateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public SelectionPageCreateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<SelectionPage> HandleAsync(SelectionPageCreateCommand command)
        {
            var category = new SelectionPage
            {
                OrganizationId = command.OrganizationId,
                Name = command.Name,
                IdName = command.IdName,
                Weight = command.Weight,
                Enabled = command.Enabled,
            };
            
            m_dataStore.Add(category);
            await m_dataStore.SaveChangesAsync();
            
            return category;
        }
    }
}
