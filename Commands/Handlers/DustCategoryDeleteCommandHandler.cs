﻿using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Models;

namespace Juliapos.Portal.ProductApi.Commands.Handlers
{
    /// <summary>
    /// Handler for the <see cref="DustCategoryDeleteCommand"/>
    /// </summary>
    public sealed class DustCategoryDeleteCommandHandler : IHandleCommand<DustCategoryDeleteCommand, DustCategory>
    {
        private readonly IApiDbDataStore m_dataStore;

        /// <summary>
        /// Create an instance of type <see cref="DustCategoryDeleteCommandHandler"/>
        /// </summary>
        /// <param name="dataStore"></param>
        public DustCategoryDeleteCommandHandler(IApiDbDataStore dataStore)
        {
            m_dataStore = dataStore;
        }

        /// <inheritdoc />
        public async Task<DustCategory> HandleAsync(DustCategoryDeleteCommand command)
        {
            var existingCategory = await m_dataStore.DustCategoryDataQuery
                .WhereOrganizationId(command.OrganizationId)
                .WhereId(command.Id)
                .SingleOrDefaultAsync();

            if (existingCategory != null)
            {
                var productsExist = m_dataStore.ProductDataQuery
                    .WhereDustCategoryId(command.Id)
                    .AsQueryable()
                    .Any();

                if (productsExist)
                    throw new HttpConflictException(ApiErrorCode.DustCategoryHasProducts, command.Id);

                m_dataStore.Remove(existingCategory);
                await m_dataStore.SaveChangesAsync();
            }

            return existingCategory;
        }
    }
}
