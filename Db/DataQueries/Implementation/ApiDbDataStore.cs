using Juliapos.Patterns.DataAccess;

namespace Juliapos.Portal.ProductApi.Db.DataQueries.Implementation
{
    public class ApiDbDataStore : DataStore, IApiDbDataStore
    {
        private readonly IApiDbContext m_apiDbContext;

        public IOrganizationDataQuery OrganizationDataQuery { get; }
        public ILocationDataQuery LocationDataQuery { get; }
        public IProductCategoryDataQuery ProductCategoryDataQuery { get; }
        public IDustCategoryDataQuery DustCategoryDataQuery { get; }
        public IMenuCategoryDataQuery MenuCategoryDataQuery { get; }
        public IProductDataQuery ProductDataQuery { get; }
        public ISelectionPageDataQuery SelectionPageDataQuery { get; }
        public ICustomAttributeDataQuery CustomAttributeDataQuery { get; }

        public ApiDbDataStore(IApiDbContext apiDbContext)
            : base(apiDbContext)
        {
            m_apiDbContext = apiDbContext;

            OrganizationDataQuery = new OrganizationDataQuery(m_apiDbContext);
            LocationDataQuery = new LocationDataQuery(m_apiDbContext);
            CustomAttributeDataQuery = new CustomAttributeDataQuery(m_apiDbContext);
            DustCategoryDataQuery = new DustCategoryDataQuery(m_apiDbContext);
            MenuCategoryDataQuery = new MenuCategoryDataQuery(m_apiDbContext);
            ProductCategoryDataQuery = new ProductCategoryDataQuery(m_apiDbContext);
            SelectionPageDataQuery = new SelectionPageDataQuery(m_apiDbContext);
            ProductDataQuery = new ProductDataQuery(m_apiDbContext);
        }

        //public IQueryable<PackagesAtResult> GetPackagesAt(DateTime at, Guid? OrganizationId, Guid? locationId)
        //    => m_apiDbContext.GetPackagesAt(at, OrganizationId, locationId);

    }
}
