using Personality.Persistence.Context;
using Personality.Reading.Persistence.QueryObject;
using Personality.Reading.Query.GetIdentityDocumentHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Personality.Persistence.QueryObject
{
    /// <summary>
    /// <inheritdoc cref="IIdentityDocumentHistoryQueryObject"/>
    /// </summary>
    public class IdentityDocumentHistoryQueryObject : IIdentityDocumentHistoryQueryObject
    {
        private readonly PersonalityContext personalityContext;

        public IdentityDocumentHistoryQueryObject(PersonalityContext personalityContext)
        {
            this.personalityContext = personalityContext ?? throw new ArgumentNullException(nameof(personalityContext));
        }

        public async Task<IList<IdentityDocumentHistoryView>> GetAsync(long personId)
        {
            return await personalityContext.IdentityDocumentHistories
                .Where(d => d.PersonId == personId)
                .Select(d => new IdentityDocumentHistoryView
                {
                    Id = d.Id,
                    StatusValue = d.StateStatusValue,
                    StatusName = d.StateStatusName,
                    PersonId = d.PersonId,
                    IdentityDocumentTypeValue = d.IdentityDocumentTypeValue,
                    IdentityDocumentTypeName = d.IdentityDocumentTypeName,
                    Serial = d.Serial,
                    Number = d.Number,
                    ValidityDateStart = d.ValidityDateStart,
                    ValidityDateEnd = d.ValidityDateEnd,
                    WhoIssued = d.WhoIssued,
                    IssuedCountryValue = d.IssuedCountryValue,
                    IssuedCountryName = d.IssuedCountryName,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate
                })
                .ToListAsync();
        }
    }
}
