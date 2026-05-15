using System;
using UnityEngine;

namespace KulibinSpace.MessageBus.Demo {

    [Serializable]
    public class Hit {
        public float damage;
    }

    [CreateAssetMenu(fileName = "New hit message", menuName = "Kulibin Space/MessageBus/Demo/Hit message")]
    public class HitMessage : AbstractMessage<Hit> {
    }

    [Serializable]
    public class HitEventItem : EventItem<Hit> {
    }

}
