using Juliapos.AspNetCore.Authorization;
using Juliapos.Patterns.ArgumentChecking;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Services.Implementation
{
    public sealed class EndpointArgumentValidator : IEndpointArgumentValidator
    {
        private readonly IApiDbDataStore m_dataStore;
        private readonly IAuthorizationContext m_authorizationContext;

        /// <summary>
        /// Create an instance of type <see cref="OrganizationsController"/>
        /// </summary>
        /// <param name="dataStore"></param>
        /// <param name="authorizationContext"></param>
        public EndpointArgumentValidator(IApiDbDataStore dataStore, IAuthorizationContext authorizationContext)
        {
            m_dataStore = dataStore;
            m_authorizationContext = authorizationContext;
        }

        /// <inheritdoc />
        public async Task<Organization> ValidateCurrentOrganizationAsync()
        {
            if (m_authorizationContext?.OrganizationId == null)
                throw new HttpForbidException();

            var organization = await m_dataStore.OrganizationDataQuery
                .WhereExternalId(m_authorizationContext.OrganizationId.Value)
                .SingleOrDefaultAsync();

            if (organization == null)
                throw new HttpForbidException();

            return organization;
        }

        /// <inheritdoc />
        public async Task<DustCategory> ValidateDustCategoryAsync(Guid categoryId)
        {
            Guard.ArgumentNotDefaultValue(categoryId, nameof(categoryId));

            if (m_authorizationContext?.OrganizationId == null)
                throw new HttpForbidException();

            var category = await m_dataStore.DustCategoryDataQuery
                .WhereId(categoryId)
                .WhereOrganizationExternalId(m_authorizationContext.OrganizationId.Value)
                .SingleOrDefaultAsync();

            if (category == null)
                throw new HttpNotFoundException(ApiErrorCode.DustCategoryNotFound, categoryId);

            return category;
        }

        /// <inheritdoc />
        public async Task<Location> ValidateLocationAsync(Guid locationId)
        {
            Guard.ArgumentNotDefaultValue(locationId, nameof(locationId));

            if (m_authorizationContext?.OrganizationId == null)
                throw new HttpForbidException();

            var location = await m_dataStore.LocationDataQuery
                .WhereId(locationId)
                .WhereOrganizationExternalId(m_authorizationContext.OrganizationId.Value)
                .SingleOrDefaultAsync();

            if (location == null)
                throw new HttpNotFoundException(ApiErrorCode.LocationNotFound, locationId);

            return location;
        }

        /// <inheritdoc />
        public async Task<MenuCategory> ValidateMenuCategoryAsync(Guid categoryId)
        {
            Guard.ArgumentNotDefaultValue(categoryId, nameof(categoryId));

            if (m_authorizationContext?.OrganizationId == null)
                throw new HttpForbidException();

            var category = await m_dataStore.MenuCategoryDataQuery
                .WhereId(categoryId)
                .WhereOrganizationExternalId(m_authorizationContext.OrganizationId.Value)
                .SingleOrDefaultAsync();

            if (category == null)
                throw new HttpNotFoundException(ApiErrorCode.MenuCategoryNotFound, categoryId);

            return category;
        }

        /// <inheritdoc />
        public async Task<Product> ValidateProductAsync(Guid productId)
        {
            Guard.ArgumentNotDefaultValue(productId, nameof(productId));

            if (m_authorizationContext?.OrganizationId == null)
                throw new HttpForbidException();

            var product = await m_dataStore.ProductDataQuery
                .WhereId(productId)
                .WhereOrganizationExternalId(m_authorizationContext.OrganizationId.Value)
                .SingleOrDefaultAsync();

            if (product == null)
                throw new HttpNotFoundException(ApiErrorCode.ProductNotFound, productId);

            return product;
        }

        /// <inheritdoc />
        public async Task<ProductCategory> ValidateProductCategoryAsync(Guid categoryId)
        {
            Guard.ArgumentNotDefaultValue(categoryId, nameof(categoryId));

            if (m_authorizationContext?.OrganizationId == null)
                throw new HttpForbidException();

            var category = await m_dataStore.ProductCategoryDataQuery
                .WhereId(categoryId)
                .WhereOrganizationExternalId(m_authorizationContext.OrganizationId.Value)
                .SingleOrDefaultAsync();

            if (category == null)
                throw new HttpNotFoundException(ApiErrorCode.ProductCategoryNotFound, categoryId);

            return category;
        }

        /// <inheritdoc />
        public async Task<CustomAttribute> ValidateCustomAttributeAsync(Guid attributeId)
        {
            Guard.ArgumentNotDefaultValue(attributeId, nameof(attributeId));

            if (m_authorizationContext?.OrganizationId == null)
                throw new HttpForbidException();

            var attribute = await m_dataStore.CustomAttributeDataQuery
                .WhereId(attributeId)
                .WhereOrganizationExternalId(m_authorizationContext.OrganizationId.Value)
                .SingleOrDefaultAsync();

            if (attribute == null)
                throw new HttpNotFoundException(ApiErrorCode.CustomAttributeNotFound, attributeId);

            return attribute;
        }

        /// <inheritdoc />
        public async Task<SelectionPage> ValidateSelectionPageAsync(Guid pageId)
        {
            Guard.ArgumentNotDefaultValue(pageId, nameof(pageId));

            if (m_authorizationContext?.OrganizationId == null)
                throw new HttpForbidException();

            var category = await m_dataStore.SelectionPageDataQuery
                .WhereId(pageId)
                .WhereOrganizationExternalId(m_authorizationContext.OrganizationId.Value)
                .SingleOrDefaultAsync();

            if (category == null)
                throw new HttpNotFoundException(ApiErrorCode.SelectionPageNotFound, pageId);

            return category;
        }
    }
}
