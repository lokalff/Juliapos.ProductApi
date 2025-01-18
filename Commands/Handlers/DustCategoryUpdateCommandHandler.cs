using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    public sealed class DustCategoryUpdateCommandHandler : IHandleCommand<DustCategoryUpdateCommand, DustCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="DustCategoryUpdateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public DustCategoryUpdateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<DustCategory> HandleAsync(DustCategoryUpdateCommand command)
        {
            var existingCategory = await m_dataStore.DustCategoryDataQuery
                .WhereId(command.Id)
                .WhereOrganizationId(command.OrganizationId)
                .SingleOrDefaultAsync() 
                ?? throw new HttpNotFoundException(ApiErrorCode.DustCategoryNotFound, command.Id);

            existingCategory.Name = command.Name;
            existingCategory.Weight = command.Weight;
            
            await m_dataStore.SaveChangesAsync();
            
            return existingCategory;
        }
    }
}
