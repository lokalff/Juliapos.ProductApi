using System.Net;

namespace Juliapos.Portal.ProductApi.Models
{
    /// <summary>
    /// Factory for error results
    /// </summary>
    public static class ErrorResultFactory
    {
        private static readonly Dictionary<ApiErrorCode, string> m_errorMap = new()
        {
            { ApiErrorCode.Forbidden, "This operation is not allowed" },

            { ApiErrorCode.OrganizationNotFound, "Organization not found" },
            { ApiErrorCode.OrganizationAlreadyExists, "Organization already exists" },
            { ApiErrorCode.OrganizationIsCurrent, "The organization is the current (logged on) organization" },

            { ApiErrorCode.LocationNotFound, "Location not found" },
            { ApiErrorCode.LocationExists, "Location already exists" },
            { ApiErrorCode.LocationHasDependencies, "Location has shipments or checkins" },

            { ApiErrorCode.ProductNotFound, "Product not found" },
            { ApiErrorCode.ProductExists, "Product already exists" },
            { ApiErrorCode.ProductVariationDuplicateLocation, "Product has duplicate location connections" },

            { ApiErrorCode.ProductCategoryNotFound, "Product category not found" },
            { ApiErrorCode.ProductCategoryHasProducts, "Product category has connected products" },

            { ApiErrorCode.DustCategoryNotFound, "Dust category not found" },
            { ApiErrorCode.DustCategoryHasProducts, "Dust category has connected products" },
            
            { ApiErrorCode.MenuCategoryNotFound, "Menu category not found" },
            { ApiErrorCode.MenuCategoryHasProducts, "Menu category has connected products" },

            //{ ApiErrorCode.PackageTypeNotFound, "Package type not found" },
            //{ ApiErrorCode.ProductNotFound, "Product not found" },
            //{ ApiErrorCode.LocationEnclosingPackageConflict, "Location and enclosing package cannot both be assigned or both empty" },
            //{ ApiErrorCode.PackageExists, "Package already exists" },
            //{ ApiErrorCode.AmountContainerConflict, "Container packages cannot have an 'Amount'" },
            //{ ApiErrorCode.EnclosingPackageNotContainer, "Enclosing package is not a package container" },
            //{ ApiErrorCode.PackageNotCheckedIn, "Package has no location" },

            //{ ApiErrorCode.DayOrderExists, "Day order already exists" },
            //{ ApiErrorCode.DayOrderNotFound, "Day order not found" },
            //{ ApiErrorCode.DayOrderParametersNotComplete, "Supplied parameters are not complete" },
        };


        /// <summary>
        /// Create an error result object
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="id"></param>
        /// <param name="errorCode"></param>
        /// <param name="extraDescription"></param>
        /// <returns></returns>
        public static ErrorResultDto CreateErrorResult(HttpStatusCode statusCode, Guid id, ApiErrorCode errorCode, string extraDescription)
        {
            string description;

            if (!m_errorMap.ContainsKey(errorCode))
                description = $"Unknown error code: {errorCode}";
            else
                description = m_errorMap[errorCode];

            if (!string.IsNullOrEmpty(extraDescription))
                description += " " + extraDescription;

            return new ErrorResultDto
            {
                StatusCode = statusCode,
                Id = id,
                errorCode = (int)errorCode,
                Description = description
            };
        }

        /// <summary>
        /// Create an error result object from an exception
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static ErrorResultDto CreateErrorResult(ApiResultException exception)
        {
            string description;

            if (!m_errorMap.ContainsKey((ApiErrorCode)exception.ErrorCode))
                description = $"Unknown error code: {exception.ErrorCode}";
            else
                description = m_errorMap[(ApiErrorCode)exception.ErrorCode];

            if (!string.IsNullOrEmpty(exception.ExtraDescription))
                description += " " + exception.ExtraDescription;

            return new ErrorResultDto
            {
                StatusCode = exception.StatusCode,
                Id = exception.Id,
                errorCode = exception.ErrorCode,
                Description = description
            };
        }



        /// <summary>
        /// Create an not found error result
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorCode"></param>
        /// <param name="extraDescription"></param>
        /// <returns></returns>
        public static ErrorResultDto NotFound(Guid id, ApiErrorCode errorCode, string extraDescription = null)
            => CreateErrorResult(HttpStatusCode.NotFound, id, errorCode, extraDescription);

        /// <summary>
        /// Create a bad request error result
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorCode"></param>
        /// <param name="extraDescription"></param>
        /// <returns></returns>
        public static ErrorResultDto BadRequest(Guid id, ApiErrorCode errorCode, string extraDescription = null)
            => CreateErrorResult(HttpStatusCode.BadRequest, id, errorCode, extraDescription);

        /// <summary>
        /// Create a conflict error result
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorCode"></param>
        /// <param name="extraDescription"></param>
        /// <returns></returns>
        public static ErrorResultDto Conflict(Guid id, ApiErrorCode errorCode, string extraDescription = null)
            => CreateErrorResult(HttpStatusCode.Conflict, id, errorCode, extraDescription);

        /// <summary>
        /// Create a forbidden error result
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorCode"></param>
        /// <param name="extraDescription"></param>
        /// <returns></returns>
        public static ErrorResultDto Forbidden(Guid id, ApiErrorCode errorCode, string extraDescription = null)
            => CreateErrorResult(HttpStatusCode.Forbidden, id, errorCode, extraDescription);

    }

    // TODO move to own file!
    /// <summary>
    /// Unique code for all controller errors 
    /// </summary>
    public enum ApiErrorCode
    {
        /// <summary>
        /// No error
        /// </summary>
        None,

        /// <summary>
        /// Generic forbidden result
        /// </summary>
        Forbidden = 1000,



        /// <summary>
        /// Organization was not found
        /// </summary>
        OrganizationNotFound = 2000,

        /// <summary>
        /// Organization already exists
        /// </summary>
        OrganizationAlreadyExists,

        /// <summary>
        /// The organization is the current (logged on) organization
        /// </summary>
        OrganizationIsCurrent,

        /// <summary>
        /// Organization has related information
        /// </summary>
        OrganizationIsNotEmpty,

        /// <summary>
        /// Location was not found
        /// </summary>
        LocationNotFound = 3000,

        /// <summary>
        /// Location exists
        /// </summary>
        LocationExists,

        /// <summary>
        /// Location still has shipments or checkins
        /// </summary>
        LocationHasDependencies,


        /// <summary>
        /// Product was not found
        /// </summary>
        ProductNotFound = 4000,

        /// <summary>
        /// Product exists
        /// </summary>
        ProductExists,

        /// <summary>
        /// Duplicate location in variation
        /// </summary>
        ProductVariationDuplicateLocation,


        /// <summary>
        /// Product Category was not found
        /// </summary>
        ProductCategoryNotFound = 5000,

        /// <summary>
        /// Product Category has connected products
        /// </summary>
        ProductCategoryHasProducts = 5001,

        /// <summary>
        /// Dust Category was not found
        /// </summary>
        DustCategoryNotFound = 5100,

        /// <summary>
        /// Dust Category has connected products
        /// </summary>
        DustCategoryHasProducts = 5101,

        /// <summary>
        /// Menu Category was not found
        /// </summary>
        MenuCategoryNotFound = 5200,

        /// <summary>
        /// Dust Category has connected products
        /// </summary>
        MenuCategoryHasProducts = 5201,

        /// <summary>
        /// Property was not found
        /// </summary>
        PropertyNotFound = 5300,

        /// <summary>
        /// Selection page was not found
        /// </summary>
        SelectionPageNotFound = 5400,


        ///// <summary>
        ///// Day order already exists
        ///// </summary>
        //DayOrderExists = 6000,

        ///// <summary>
        ///// Day order not found
        ///// </summary>
        //DayOrderNotFound,

        ///// <summary>
        ///// Supplied parameters are not complete
        ///// </summary>
        //DayOrderParametersNotComplete,
    };
}
