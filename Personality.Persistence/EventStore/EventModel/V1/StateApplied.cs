using System;

namespace Personality.Persistence.EventStore.EventModel.V1
{
    public class StateApplied : PersonEventBase
    {
        private StateApplied()
        { }

        public StateApplied(long id, long personId, long stateId, string description, DateTime changeDate, DateTime occurredAt)
            : base(id, personId, stateId, description, changeDate, occurredAt)
        {
        }
    }
}
