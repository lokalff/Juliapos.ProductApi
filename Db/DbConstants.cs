namespace Juliapos.Portal.ProductApi.Db
{
    public sealed class DbConstants
    {
        public const string MigrationsAssemblyPrefix = "Juliapos.Portal.ProductApi.Db.";

        public const string DefaultSchema = "product";
        public const string HistoryTableName = "__EFMigrationHistory";


        public static class Table
        {
            public const string DustCategory = "dustcategory";
            public const string MenuCategory = "menucategory";
            public const string Location = "location";
            public const string Organization = "organization";
            public const string ProductCategory = "productcategory";
            public const string Property = "property";
            public const string PropertyValue = "propertyvalue";
            public const string SelectionPage = "selectionpage";
            public const string SelectionPageProduct = "selectionpageproduct";
            public const string Product = "product";
            public const string ProductVariation = "productvariation";
            public const string ProductLocation = "productlocation";
            public const string ProductVariationLocation = "productvariationlocation";
            public const string MenuCategoryProperty = "menucategoryproperty";
        }

        public static class View
        {
        }

        public static class Function
        {
        }
    }
}
