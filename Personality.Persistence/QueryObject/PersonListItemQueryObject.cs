using Microsoft.EntityFrameworkCore;
using Personality.Persistence.Context;
using Personality.Reading.Persistence.QueryObject;
using Personality.Reading.Query.GetPersonListItem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personality.Persistence.QueryObject
{
    /// <summary>
    /// <inheritdoc cref="IPersonListItemQueryObject"/>
    /// </summary>
    public class PersonListItemQueryObject : IPersonListItemQueryObject
    {
        private readonly PersonalityContext personalityContext;

        public PersonListItemQueryObject(PersonalityContext personalityContext)
        {
            this.personalityContext = personalityContext ?? throw new System.ArgumentNullException(nameof(personalityContext));
        }

        public async Task<IList<PersonListItemView>> GetAsync(/*LightReadParams<PersonListItemView> lightReadParams,*/ bool includeWorkState)
        {
            var query = from personalData in personalityContext.PersonalDataHistories
                join identityDocument in personalityContext.IdentityDocumentHistories on personalData.PersonId equals identityDocument.PersonId into identityDocumentLeft
                from identityDocument in identityDocumentLeft.DefaultIfEmpty()
                select new PersonListItemView
                {
                    Id = personalData.Id,
                    PersonId = personalData.PersonId,
                    StatusValue = personalData.StateStatusValue == "raw" || identityDocument.StateStatusValue == "raw" ? "raw" :
                                  personalData.StateStatusValue == "approved" || identityDocument.StateStatusValue == "approved" ? "approved" :
                                  personalData.StateStatusValue == "canceled" || identityDocument.StateStatusValue == "canceled" ? "canceled" :
                                  personalData.StateStatusValue == "applied" || identityDocument.StateStatusValue == "applied" ? "applied" : personalData.StateStatusValue,
                    LastName = personalData.LastName,
                    FirstName = personalData.FirstName,
                    MiddleName = personalData.MiddleName,
                    Birthday = personalData.Birthday,
                    DeathDate = personalData.DeathDate,
                    SexValue = personalData.SexValue,
                    SexName = personalData.SexName,
                    CitizenshipValue = personalData.CitizenshipValue,
                    CitizenshipName = personalData.CitizenshipName,
                    IdentityDocumentTypeValue = identityDocument.IdentityDocumentTypeValue,
                    IdentityDocumentTypeName = identityDocument.IdentityDocumentTypeName,
                    IdentityDocumentSerial = identityDocument.Serial,
                    IdentityDocumentNumber = identityDocument.Number
                };

            //query = query.ApplyFilter(lightReadParams);

            if (!includeWorkState)
                query = query.Where(p => p.StatusValue == "applied");

            return await query.ToListAsync();
        }
    }
}
