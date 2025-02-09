// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace Juliapos.Portal.ProductsApi.Models
{
    /// <summary>
    /// DTO for product selection page
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SelectionPageDto : IParsable
    {
        /// <summary>Enabled</summary>
        public bool? Enabled { get; set; }
        /// <summary>Id</summary>
        public Guid? Id { get; set; }
        /// <summary>Id as a name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdName { get; set; }
#nullable restore
#else
        public string IdName { get; set; }
#endif
        /// <summary>Name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>Weight (order)</summary>
        public int? Weight { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Juliapos.Portal.ProductsApi.Models.SelectionPageDto();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "enabled", n => { Enabled = n.GetBoolValue(); } },
                { "id", n => { Id = n.GetGuidValue(); } },
                { "idName", n => { IdName = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "weight", n => { Weight = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("enabled", Enabled);
            writer.WriteGuidValue("id", Id);
            writer.WriteStringValue("idName", IdName);
            writer.WriteStringValue("name", Name);
            writer.WriteIntValue("weight", Weight);
        }
    }
}
#pragma warning restore CS0618
