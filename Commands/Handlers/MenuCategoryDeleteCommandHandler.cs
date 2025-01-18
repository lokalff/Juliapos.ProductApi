using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    public sealed class MenuCategoryDeleteCommandHandler : IHandleCommand<MenuCategoryDeleteCommand, MenuCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="MenuCategoryDeleteCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public MenuCategoryDeleteCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<MenuCategory> HandleAsync(MenuCategoryDeleteCommand command)
        {
            var existingCategory = await m_dataStore.MenuCategoryDataQuery
                .WhereId(command.Id)
                .SingleOrDefaultAsync();

            if (existingCategory != null)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .WhereDustCategoryId(command.Id)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.MenuCategoryHasProducts, command.Id);

                m_dataStore.Remove(existingCategory);
                await m_dataStore.SaveChangesAsync();
            }

            return existingCategory;
        }
    }
}
