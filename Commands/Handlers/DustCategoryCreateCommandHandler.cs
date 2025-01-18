using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="DustCategoryCreateCommand"/>
    /// </summary>
    public sealed class DustCategoryCreateCommandHandler : IHandleCommand<DustCategoryCreateCommand, DustCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="DustCategoryCreateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public DustCategoryCreateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<DustCategory> HandleAsync(DustCategoryCreateCommand command)
        {
            var category = new DustCategory
            {
                OrganizationId = command.OrganizationId,
                Name = command.Name,
                Weight = command.Weight,
            };
            
            m_dataStore.Add(category);
            await m_dataStore.SaveChangesAsync();
            
            return category;
        }
    }
}
