
using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Commands
{
    /// <summary>
    /// Command to create a <see cref="Property"/>
    /// </summary>
    public sealed class PropertyCreateCommand : ICommand<Property>
    {
        /// <summary>
        /// Organization id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id as a name
        /// </summary>
        public string IdName { get; set; }

        /// <summary>
        /// TYpe name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; }

    }
}
