namespace Juliapos.Portal.ProductApi.Api.Models.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ProductSelectionPageReferenceDto
    {
        public Guid SelectionPageId { get; set; }
        public string ForeColor { get; set; }
        public string BackColor { get; set; }
        public int RowIdx { get; set; }
        public int ColumnIdx { get; set; }
    }
}
