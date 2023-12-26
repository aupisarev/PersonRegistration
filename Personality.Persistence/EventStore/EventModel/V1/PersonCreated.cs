using System;

namespace Personality.Persistence.EventStore.EventModel.V1
{
    public class PersonCreated : PersonEventBase
    {
        private PersonCreated()
        { }

        public PersonCreated(long id, long personId, long stateId, string description, DateTime changeDate, DateTime occurredAt)
            : base(id, personId, stateId, description, changeDate, occurredAt)
        {
        }
    }
}
