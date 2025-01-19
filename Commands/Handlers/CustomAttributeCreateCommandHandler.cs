using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="CustomAttributeCreateCommand"/>
    /// </summary>
    public sealed class CustomAttributeCreateCommandHandler : IHandleCommand<CustomAttributeCreateCommand, CustomAttribute>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="CustomAttributeCreateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public CustomAttributeCreateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<CustomAttribute> HandleAsync(CustomAttributeCreateCommand command)
        {
            var attribute = new CustomAttribute
            {
                OrganizationId = command.OrganizationId,
                Name = command.Name,
                IdName = command.IdName,
                TypeName = command.TypeName,
                Enabled = command.Enabled,
            };
            
            m_dataStore.Add(attribute);
            await m_dataStore.SaveChangesAsync();
            
            return attribute;
        }
    }
}
