using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="MenuCategoryCreateCommand"/>
    /// </summary>
    public sealed class MenuCategoryCreateCommandHandler : IHandleCommand<MenuCategoryCreateCommand, MenuCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="MenuCategoryCreateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public MenuCategoryCreateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<MenuCategory> HandleAsync(MenuCategoryCreateCommand command)
        {
            var category = new MenuCategory
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
