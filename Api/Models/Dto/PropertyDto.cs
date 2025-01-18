namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for Properties
    /// </summary>
    public sealed class PropertyDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id as a name
        /// </summary>
        public string IdName { get; set; }

        /// <summary>
        /// Typename
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; }

    }
}
