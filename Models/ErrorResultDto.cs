using System.Net;
using System.Text.Json.Serialization;


namespace Juliapos.Portal.ProductApi.Models
{
    /// <summary>
    /// Result used to describe errors
    /// </summary>
    public sealed class ErrorResultDto
    {
        /// <summary>
        /// Statuscode that was resturned with this error
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Id of the object that caused this error
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }

        /// <summary>
        /// Api error code 
        /// </summary>
        public int errorCode { get; set; }

        /// <summary>
        /// User friendly description of the error
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Create an error result object
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ErrorResultDto CreateErrorResult(HttpStatusCode statusCode, Guid id, string description)
        {
            return new ErrorResultDto
            {
                StatusCode = statusCode,
                Id = id,
                Description = description
            };
        }
    }
}
