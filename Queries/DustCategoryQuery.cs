﻿using Juliapos.Patterns.CQRS.Queries;
using Juliapos.Portal.ProductApi.Db.Models;

namespace Juliapos.Portal.ProductApi.Queries
{
    /// <summary>
    /// Query to retrieve a dust category
    /// </summary>
    public sealed class DustCategoryQuery : IQuery<DustCategory>
    {
        /// <summary>
        /// Organization id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
    }
}
