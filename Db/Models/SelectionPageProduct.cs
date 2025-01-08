namespace Juliapos.Portal.ProductApi.Db.Models
{
    public sealed class SelectionPageProduct
    {
        public Guid SelectionPageProductId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SelectionPageId { get; set; }
        public string ForeColor { get; set; }
        public string BackColor { get; set; }
        public int RowIdx { get; set; }
        public int ColumnIdx { get; set; }

        public Product Product { get; set; }
        public SelectionPage SelectionPage { get; set; }

    }
}
