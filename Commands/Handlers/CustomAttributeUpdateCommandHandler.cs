using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="CustomAttributeUpdateCommand"/>
    /// </summary>
    public sealed class CustomAttributeUpdateCommandHandler : IHandleCommand<CustomAttributeUpdateCommand, CustomAttribute>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="CustomAttributeUpdateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public CustomAttributeUpdateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<CustomAttribute> HandleAsync(CustomAttributeUpdateCommand command)
        {
            var existingAttribute = await m_dataStore.CustomAttributeDataQuery
                .WhereId(command.Id)
                .WhereOrganizationId(command.OrganizationId)
                .SingleOrDefaultAsync() 
                ?? throw new HttpNotFoundException(ApiErrorCode.CustomAttributeNotFound, command.Id);

            if (!command.Enabled && existingAttribute.Enabled)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .WhereMenuCategoryId(existingAttribute.CustomAttributeId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.CustomAttributeHasProducts, existingAttribute.CustomAttributeId);
            }

            existingAttribute.Name = command.Name;
            existingAttribute.TypeName = command.TypeName;
            existingAttribute.Enabled = command.Enabled;

            await m_dataStore.SaveChangesAsync();
            
            return existingAttribute;
        }
    }
}
