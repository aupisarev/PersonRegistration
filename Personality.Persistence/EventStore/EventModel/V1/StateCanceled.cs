using System;

namespace Personality.Persistence.EventStore.EventModel.V1
{
    public class StateCanceled : PersonEventBase
    {
        private StateCanceled()
        { }

        public StateCanceled(long id, long personId, long stateId, string description, DateTime changeDate, DateTime occurredAt)
            : base(id, personId, stateId, description, changeDate, occurredAt)
        {
        }
    }
}
