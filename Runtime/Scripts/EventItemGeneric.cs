using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace KulibinSpace.MessageBus {

    [Serializable]
    public abstract class EventItem<T> : EventItem {

        [SerializeField]
        public UnityEvent<T> broadcast = new();
        private Action<T> cachedDelegate;

        public override void Subscribe (AbstractGameMessage message) {
            if (message is not AbstractMessage<T> typedMessage) return;
            cachedDelegate = OnMessage;
            typedMessage.Event += cachedDelegate;
        }

        public override void Unsubscribe (AbstractGameMessage message) {
            if (message is not AbstractMessage<T> typedMessage) return;
            typedMessage.Event -= cachedDelegate;
        }

        private void OnMessage (T value) {
            broadcast?.Invoke(value);
        }
    }

}
