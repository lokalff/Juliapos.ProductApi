using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    public sealed class ProductCategoryUpdateDto
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public MeasureMethod MeasureMethod { get; set; }           // None, Count, Weigh
        public string DefaultForeColor { get; set; }
        public string DefaultBackColor { get; set; }
        public bool Enabled { get; set; }
    }
}
