using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KulibinSpace.MessageBus {

    public abstract class EventItem {
        public abstract void Subscribe (AbstractGameMessage message);
        public abstract void Unsubscribe (AbstractGameMessage message);
    }
}


