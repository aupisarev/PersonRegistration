using Microsoft.EntityFrameworkCore;
using Personality.Persistence.Context;
using Personality.Reading.Persistence.QueryObject;
using Personality.Reading.Query.GetPersonalDataHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personality.Persistence.QueryObject
{
    /// <summary>
    /// <inheritdoc cref="IPersonalDataHistoryQueryObject"/>
    /// </summary>
    public class PersonalDataHistoryQueryObject : IPersonalDataHistoryQueryObject
    {
        private readonly PersonalityContext personalityContext;

        public PersonalDataHistoryQueryObject(PersonalityContext personalityContext)
        {
            this.personalityContext = personalityContext ?? throw new ArgumentNullException(nameof(personalityContext));
        }

        public async Task<IList<PersonalDataHistoryView>> GetAsync(long personId)
        {
            return await personalityContext.PersonalDataHistories
                .Where(d => d.PersonId == personId)
                .Select(d => new PersonalDataHistoryView
                {
                    Id = d.Id,
                    StatusValue = d.StateStatusValue,
                    StatusName = d.StateStatusName,
                    PersonId = d.PersonId,
                    LastName = d.LastName,
                    FirstName = d.FirstName,
                    MiddleName = d.MiddleName,
                    Birthday = d.Birthday,
                    DeathDate = d.DeathDate,
                    SexValue = d.SexValue,
                    SexName = d.SexName,
                    CitizenshipValue = d.CitizenshipValue,
                    CitizenshipName = d.CitizenshipName,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate
                })
                .ToListAsync();
        }
    }
}
