using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for updating product categories
    /// </summary>
    public sealed class ProductCategoryUpdateDto
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
        /// Measure method
        /// </summary>
        public MeasureMethod MeasureMethod { get; set; }           // None, Count, Weigh

        /// <summary>
        /// Default foreground color for new products
        /// </summary>
        public string DefaultForeColor { get; set; }


        /// <summary>
        /// Default background color for new products
        /// </summary>
        public string DefaultBackColor { get; set; }

        /// <summary>
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; }

    }
}
