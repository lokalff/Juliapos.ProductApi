using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="PropertyUpdateCommand"/>
    /// </summary>
    public sealed class PropertyUpdateCommandHandler : IHandleCommand<PropertyUpdateCommand, Property>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="PropertyUpdateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public PropertyUpdateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<Property> HandleAsync(PropertyUpdateCommand command)
        {
            var existingProperty = await m_dataStore.PropertyDataQuery
                .WhereId(command.Id)
                .WhereOrganizationId(command.OrganizationId)
                .SingleOrDefaultAsync() 
                ?? throw new HttpNotFoundException(ApiErrorCode.PropertyNotFound, command.Id);

            if (!command.Enabled && existingProperty.Enabled)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .WhereMenuCategoryId(existingProperty.PropertyId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.PropertyHasProducts, existingProperty.PropertyId);
            }

            existingProperty.Name = command.Name;
            existingProperty.TypeName = command.TypeName;
            existingProperty.Enabled = command.Enabled;

            await m_dataStore.SaveChangesAsync();
            
            return existingProperty;
        }
    }
}
