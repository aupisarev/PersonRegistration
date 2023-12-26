using Microsoft.EntityFrameworkCore;
using Personality.Persistence.Context;
using Personality.Reading.Persistence.Repository;
using Personality.Reading.Projecting.PersonStateModel.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Personality.Persistence.Repository
{
    public class PersonStateProjectionRepository : IPersonStateProjectionRepository, IContextObserver
    {
        private bool cleared;

        public async Task<IList<PersonStateProjection>> GetAsync(Expression<Func<PersonStateProjection, bool>> condition, IUnitOfWork unitOfWork)
        {
            var ctx = GetContext(unitOfWork);

            if (cleared)
                return ctx.PersonStates.Local.Where(condition.Compile()).ToList();

            return await GetContext(unitOfWork).PersonStates.Where(condition).ToListAsync();
        }

        public async Task InsertAsync(PersonStateProjection projection, IUnitOfWork unitOfWork)
        {
            await GetContext(unitOfWork).AddAsync(projection);
        }

        public Task UpdateAsync(PersonStateProjection projection, IUnitOfWork unitOfWork)
        {
            var ctx = GetContext(unitOfWork);

            var existing = ctx.PersonStates.Local.FirstOrDefault(e => e.Id == projection.Id);
            if (existing != null && !ReferenceEquals(existing, projection))
            {
                var existingState = ctx.Entry(existing).State;
                ctx.Entry(existing).State = EntityState.Detached;
                ctx.Entry(projection).State = existingState;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(PersonStateProjection projection, IUnitOfWork unitOfWork)
        {
            GetContext(unitOfWork).Remove(projection);
            return Task.CompletedTask;
        }

        public Task ClearAsync(IUnitOfWork unitOfWork)
        {
            var ctx = GetContext(unitOfWork);

            ctx.PersonStates.Local.Clear();
            ctx.Clear<PersonStateProjection>();
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
