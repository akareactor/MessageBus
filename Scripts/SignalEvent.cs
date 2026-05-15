using System;
using UnityEngine;
using UnityEngine.Events;

namespace KulibinSpace.MessageBus {

    [System.Serializable]
    public class SignalEvent : UnityEvent {
    }

    [Serializable]
    public class SignalEventItem : EventItem {

        [SerializeReference]
        public UnityEvent broadcast = new SignalEvent();

        private Action cachedDelegate;

        public override void Subscribe(AbstractGameMessage message) {

            if (message is not AbstractMessage typedMessage)
                return;

            cachedDelegate = OnMessage;

            typedMessage.Event += cachedDelegate;
        }

        public override void Unsubscribe(AbstractGameMessage message) {

            if (message is not AbstractMessage typedMessage)
                return;

            typedMessage.Event -= cachedDelegate;
        }

        private void OnMessage() {
            broadcast?.Invoke();
        }
    }

}
