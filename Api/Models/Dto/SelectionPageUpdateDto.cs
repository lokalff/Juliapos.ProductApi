namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for updating a product selection page
    /// </summary>
    public sealed class SelectionPageUpdateDto
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
