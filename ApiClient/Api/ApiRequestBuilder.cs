// <auto-generated/>
#pragma warning disable CS0618
using Juliapos.Portal.ProductsApi.Api.V1;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Juliapos.Portal.ProductsApi.Api
{
    /// <summary>
    /// Builds and executes requests for operations under \api
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ApiRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The v1 property</summary>
        public global::Juliapos.Portal.ProductsApi.Api.V1.V1RequestBuilder V1
        {
            get => new global::Juliapos.Portal.ProductsApi.Api.V1.V1RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Juliapos.Portal.ProductsApi.Api.ApiRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ApiRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Juliapos.Portal.ProductsApi.Api.ApiRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ApiRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
