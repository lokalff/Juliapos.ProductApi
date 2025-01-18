using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// DTO for the product category
    /// </summary>
    public sealed class ProductCategoryDto
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
        /// Weight (order)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Id as a name
        /// </summary>
        public string IdName { get; set; }

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
