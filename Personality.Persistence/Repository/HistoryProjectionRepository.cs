using Microsoft.EntityFrameworkCore;
using Personality.Persistence.Context;
using Personality.Reading.Persistence.Repository;
using Personality.Reading.Projecting.PersonHistoryModel;
using Personality.Reading.Projecting.PersonHistoryModel.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Personality.Persistence.Repository
{
    /// <summary>
    /// <inheritdoc cref="IHistoryProjectionRepository"/>
    /// </summary>
    public class HistoryProjectionRepository : IHistoryProjectionRepository, IContextObserver
    {
        private bool cleared;

        public async Task<IList<TProjection>> GetAsync<TProjection>(Expression<Func<TProjection, bool>> condition, IUnitOfWork unitOfWork) where TProjection : class, IHistoryProjection
        {
            var ctx = GetContext(unitOfWork);

            if (cleared)
                return ctx.Set<TProjection>().Local.Where(condition.Compile()).ToList();

            return await ctx.Set<TProjection>().Where(condition).ToListAsync();
        }

        public async Task InsertAsync<TProjection>(TProjection projection, IUnitOfWork unitOfWork) where TProjection : class, IHistoryProjection
        {
            await GetContext(unitOfWork).Set<TProjection>().AddAsync(projection);
        }

        public Task UpdateAsync<TProjection>(TProjection projection, IUnitOfWork unitOfWork) where TProjection : class, IHistoryProjection
        {
            var ctx = GetContext(unitOfWork);

            var existing = ctx.Set<TProjection>().Local.FirstOrDefault(e => e.Id == projection.Id);
            if (existing != null && !ReferenceEquals(existing, projection))
            {
                var existingState = ctx.Entry(existing).State;
                ctx.Entry(existing).State = EntityState.Detached;
                ctx.Entry(projection).State = existingState;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync<TProjection>(TProjection projection, IUnitOfWork unitOfWork) where TProjection : class, IHistoryProjection
        {
            GetContext(unitOfWork).Set<TProjection>().Remove(projection);
            return Task.CompletedTask;
        }

        public Task ClearAsync(IUnitOfWork unitOfWork)
        {
            var ctx = GetContext(unitOfWork);

            ctx.PersonalDataHistories.Local.Clear();
            ctx.Clear<PersonalDataHistoryProjection>();

            ctx.IdentityDocumentHistories.Local.Clear();
            ctx.Clear<IdentityDocumentHistoryProjection>();

            cleared = true;

            return Task.CompletedTask;
        }

        private PersonalityContext GetContext(IUnitOfWork unitOfWork)
        {
            if (unitOfWork is not PersonalityContext context)
                throw new ArgumentException($"Объект unitOfWork не соответствует реализации репозитория. Ожидается объект типа {nameof(PersonalityContext)}", nameof(unitOfWork));

            context.AddObserver(this);
            return context;
        }

        public void HandleCommit()
        {
            cleared = false;
        }
    }
}
