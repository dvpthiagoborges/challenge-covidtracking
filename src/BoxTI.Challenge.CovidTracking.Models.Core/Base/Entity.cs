using System;

namespace BoxTI.Challenge.CovidTracking.Models.Core.Base
{
    public class Entity
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            Active = true;
        }
    }
}
