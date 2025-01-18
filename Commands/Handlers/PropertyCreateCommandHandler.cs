using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="PropertyCreateCommand"/>
    /// </summary>
    public sealed class PropertyCreateCommandHandler : IHandleCommand<PropertyCreateCommand, Property>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="PropertyCreateCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public PropertyCreateCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<Property> HandleAsync(PropertyCreateCommand command)
        {
            var property = new Property
            {
                OrganizationId = command.OrganizationId,
                Name = command.Name,
                IdName = command.IdName,
                TypeName = command.TypeName,
                Enabled = command.Enabled,
            };
            
            m_dataStore.Add(property);
            await m_dataStore.SaveChangesAsync();
            
            return property;
        }
    }
}
