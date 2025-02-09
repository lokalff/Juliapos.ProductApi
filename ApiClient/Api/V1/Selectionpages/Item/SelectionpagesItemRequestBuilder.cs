// <auto-generated/>
#pragma warning disable CS0618
using Juliapos.Portal.ProductsApi.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Juliapos.Portal.ProductsApi.Api.V1.Selectionpages.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \api\v1\selectionpages\{id}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SelectionpagesItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::Juliapos.Portal.ProductsApi.Api.V1.Selectionpages.Item.SelectionpagesItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SelectionpagesItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api/v1/selectionpages/{id}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Juliapos.Portal.ProductsApi.Api.V1.Selectionpages.Item.SelectionpagesItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SelectionpagesItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/api/v1/selectionpages/{id}", rawUrl)
        {
        }
        /// <summary>
        /// Delete a product selection page
        /// </summary>
        /// <returns>A <see cref="global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto">When receiving a 409 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto?> DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto> DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "409", global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto>(requestInfo, global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get product selection page with id as parameter
        /// </summary>
        /// <returns>A <see cref="global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto">When receiving a 400 status code</exception>
        /// <exception cref="global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto">When receiving a 404 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "400", global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto.CreateFromDiscriminatorValue },
                { "404", global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto>(requestInfo, global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Update a product selection page
        /// </summary>
        /// <returns>A <see cref="global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto"/></returns>
        /// <param name="body">DTO for updating a product selection page</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto">When receiving a 404 status code</exception>
        /// <exception cref="global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto">When receiving a 409 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto?> PutAsync(global::Juliapos.Portal.ProductsApi.Models.SelectionPageUpdateDto body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto> PutAsync(global::Juliapos.Portal.ProductsApi.Models.SelectionPageUpdateDto body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPutRequestInformation(body, requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "404", global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto.CreateFromDiscriminatorValue },
                { "409", global::Juliapos.Portal.ProductsApi.Models.ErrorResultDto.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto>(requestInfo, global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Delete a product selection page
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.DELETE, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Get product selection page with id as parameter
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Update a product selection page
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">DTO for updating a product selection page</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPutRequestInformation(global::Juliapos.Portal.ProductsApi.Models.SelectionPageUpdateDto body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPutRequestInformation(global::Juliapos.Portal.ProductsApi.Models.SelectionPageUpdateDto body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.PUT, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::Juliapos.Portal.ProductsApi.Api.V1.Selectionpages.Item.SelectionpagesItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Juliapos.Portal.ProductsApi.Api.V1.Selectionpages.Item.SelectionpagesItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Juliapos.Portal.ProductsApi.Api.V1.Selectionpages.Item.SelectionpagesItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SelectionpagesItemRequestBuilderDeleteRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SelectionpagesItemRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SelectionpagesItemRequestBuilderPutRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
