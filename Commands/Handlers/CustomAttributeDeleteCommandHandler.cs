using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="CustomAttributeDeleteCommand"/>
    /// </summary>
    public sealed class CustomAttributeDeleteCommandHandler : IHandleCommand<CustomAttributeDeleteCommand, CustomAttribute>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="CustomAttributeDeleteCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public CustomAttributeDeleteCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<CustomAttribute> HandleAsync(CustomAttributeDeleteCommand command)
        {
            var existingAttribute = await m_dataStore.CustomAttributeDataQuery
                .WhereOrganizationId(command.OrganizationId)
                .WhereId(command.Id)
                .SingleOrDefaultAsync();

            if (existingAttribute != null)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .HasCustomAttribute(existingAttribute.CustomAttributeId)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.CustomAttributeHasProducts, command.Id);

                m_dataStore.Remove(existingAttribute);
                await m_dataStore.SaveChangesAsync();
            }

            return existingAttribute;
        }
    }
}
