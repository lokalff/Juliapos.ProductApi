﻿using System.Net;

namespace Juliapos.Portal.ProductApi.Models
{
    /// <summary>
    /// Exception that signals the NotFound statuscode should be used in the controller
    /// </summary>
    public sealed class HttpNotFoundException : ApiResultException
    {
        /// <summary>
        /// Create an instance of type <see href="HttpNotFoundException" />
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="id"></param>
        /// <param name="description"></param>
        public HttpNotFoundException(ApiErrorCode errorCode, Guid id, string description = null)
            : base(HttpStatusCode.NotFound, (int)errorCode, id, description)
        { }
    }
}
