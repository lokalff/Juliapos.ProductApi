using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Commands
{
    /// <summary>
    /// Command to create a new <see cref="DustCategory"/>
    /// </summary>
    public sealed class DustCategoryCreateCommand : ICommand<DustCategory>
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
        /// Weight (order)
        /// </summary>
        public int Weight { get; set; }
    }
}
