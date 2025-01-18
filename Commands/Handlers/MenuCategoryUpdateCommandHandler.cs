using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    public sealed class MenuCategoryUpdateCommandHandler : IHandleCommand<MenuCategoryUpdateCommand, MenuCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="MenuCategoryUpdateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public MenuCategoryUpdateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<MenuCategory> HandleAsync(MenuCategoryUpdateCommand command)
        {
            var existingCategory = await m_dataStore.MenuCategoryDataQuery
                .WhereId(command.Id)
                .WhereOrganizationId(command.OrganizationId)
                .SingleOrDefaultAsync() 
                ?? throw new HttpNotFoundException(ApiErrorCode.MenuCategoryNotFound, command.Id);

            if (!command.Enabled && existingCategory.Enabled)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .WhereProductCategoryId(existingCategory.MenuCategoryId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.ProductCategoryHasProducts, existingCategory.MenuCategoryId);
            }


            existingCategory.Name = command.Name;
            existingCategory.Weight = command.Weight;
            existingCategory.Enabled = command.Enabled;

            await m_dataStore.SaveChangesAsync();
            
            return existingCategory;
        }
    }
}
