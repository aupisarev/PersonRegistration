using Microsoft.EntityFrameworkCore;
using Personality.Domain.OfPerson.OfState;
using Personality.Persistence.Context;
using Personality.Reading.Persistence.QueryObject;
using Personality.Reading.Query.GetPersonModification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personality.Persistence.QueryObject
{
    public class PersonModificationQueryObject : IPersonModificationQueryObject
    {
        private readonly PersonalityContext personalityContext;

        public PersonModificationQueryObject(PersonalityContext personalityContext)
        {
            this.personalityContext = personalityContext ?? throw new ArgumentNullException(nameof(personalityContext));
        }

        public async Task<GetPersonModificationResponse> GetAsync(long personId)
        {
            var states = await personalityContext.PersonStates
                .Where(s => s.PersonId == personId)
                .ToListAsync();

            if (!states.Any())
                return new GetPersonModificationResponse();

            var events = (
                    from state in states
                    from @event in state.Events
                    select new PersonEventView
                    {
                        Id = @event.EventId,
                        Compensatory = @event.Compensatory,
                        StateId = state.StateId,
                        Description = @event.Description,
                        ChangeDate = @event.ChangeDate,
                        OccurredAt = @event.OccurredAt,
                    }
                )
                .OrderByDescending(e => e.OccurredAt)
                .ThenByDescending(e => e.Id)
                .ToList();

            var lastStateId = events.MaxBy(e => e.OccurredAt).StateId;
            var lastState = states.First(s => s.StateId == lastStateId);

            return new GetPersonModificationResponse
            {
                CurrentStatusValue = lastState.StatusValue,
                CurrentStatusName = lastState.StatusName,
                WorkEvents = StateStatus.Get(lastState.StatusValue).IsFinal ? new List<PersonEventView>() : events.Where(e => e.StateId == lastStateId).ToList(),
                HistoryEvents = StateStatus.Get(lastState.StatusValue).IsFinal ? events : events.Where(e => e.StateId != lastStateId).ToList()
            };
        }
    }
}
