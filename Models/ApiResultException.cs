using System.Net;
using System.Text.Json.Serialization;

namespace Juliapos.Portal.ProductApi.Models
{
    /// <summary>
    /// Base class for all exceptions that result in a specific HttpStatusCode
    /// </summary>
    public class ApiResultException : Exception
    {

        /// <summary>
        /// Statuscode to return
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Id of the object that caused this error
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; }

        /// <summary>
        /// Api error code 
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// User friendly description of the error
        /// </summary>
        public string ExtraDescription { get; }

        /// <summary>
        /// Create an instance of type <see href="ApiResultException" />
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="errorCode"></param>
        /// <param name="id"></param>
        /// <param name="description"></param>
        protected ApiResultException(HttpStatusCode statusCode, int errorCode, Guid id, string description)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Id = id;
            ExtraDescription = description;
        }

    }
}
