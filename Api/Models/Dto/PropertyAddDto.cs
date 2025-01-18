namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for adding a property
    /// </summary>
    public sealed class PropertyAddDto
    {
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
