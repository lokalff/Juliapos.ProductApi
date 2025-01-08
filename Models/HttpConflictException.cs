using System.Net;

namespace Juliapos.Portal.ProductApi.Models
{
    /// <summary>
    /// Exception that signals the conflict statuscode should be used in the controller
    /// </summary>
    public sealed class HttpConflictException : ApiResultException
    {
        /// <summary>
        /// Create an instance of type <see href="HttpConflictException" />
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="id"></param>
        /// <param name="description"></param>
        public HttpConflictException(ApiErrorCode errorCode, Guid id, string description = null)
            : base(HttpStatusCode.Conflict, (int)errorCode, id, description) 
        {}
    }
}
