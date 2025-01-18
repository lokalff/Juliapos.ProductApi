namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for adding a Menu category
    /// </summary>
    public sealed class MenuCategoryAddDto
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Weight (order)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Id as a name
        /// </summary>
        public string IdName { get; set; }

        /// <summary>
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
