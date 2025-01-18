using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="PropertyDeleteCommand"/>
    /// </summary>
    public sealed class PropertyDeleteCommandHandler : IHandleCommand<PropertyDeleteCommand, Property>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="PropertyDeleteCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public PropertyDeleteCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<Property> HandleAsync(PropertyDeleteCommand command)
        {
            var existingProperty = await m_dataStore.PropertyDataQuery
                .WhereId(command.Id)
                .SingleOrDefaultAsync();

            if (existingProperty != null)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .HasProperty(existingProperty.PropertyId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.PropertyHasProducts, command.Id);

                m_dataStore.Remove(existingProperty);
                await m_dataStore.SaveChangesAsync();
            }

            return existingProperty;
        }
    }
}
