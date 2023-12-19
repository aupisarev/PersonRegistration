using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Reading.Projecting.PersonHistoryModel;
using Personality.Reading.Projecting.PersonStateModel;
using Personality.Writing.Outside;

namespace Personality.Reading.Projecting
{
    /// <summary>
    /// <inheritdoc cref="IProjector"/>
    /// </summary>
    public class Projector : IProjector
    {
        private readonly IList<IProjector> projectors;

        public Projector(
            PersonHistoryProjector personHistoryProjector,
            PersonStateProjector personStateProjector)
        {
            if (personHistoryProjector is null)
                throw new ArgumentNullException(nameof(personHistoryProjector));

            if (personStateProjector is null)
                throw new ArgumentNullException(nameof(personStateProjector));

            projectors = new List<IProjector>
            {
                personHistoryProjector,
                personStateProjector
            };
        }

        public async Task ProjectAsync(IList<PersonEvent> events)
        {
            foreach (var projector in projectors)
            {
                await projector.ProjectAsync(events);
            }
        }
    }
}
