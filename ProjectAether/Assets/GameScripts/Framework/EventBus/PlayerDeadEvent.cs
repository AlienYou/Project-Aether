using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework
{
    public struct PlayerDeadEvent : IEvent
    {
        public int PlayerId { get; private set; }

        public PlayerDeadEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
