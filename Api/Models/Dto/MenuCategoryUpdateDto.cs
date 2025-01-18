namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for updating a Menu category
    /// </summary>
    public sealed class MenuCategoryUpdateDto
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
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
