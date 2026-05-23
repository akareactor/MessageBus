using UnityEngine;
using UnityEngine.Events;
using System;

namespace KulibinSpace.MessageBus {

    [Serializable]
    public class VoidEventItem : EventItem {

        [SerializeField]
        public UnityEvent broadcast = new();

        private Action cachedDelegate;

        public override void Subscribe(AbstractGameMessage message) {
            if(message is not AbstractMessage typedMessage) return;
            cachedDelegate = OnMessage;
            typedMessage.Event += cachedDelegate;
        }

        public override void Unsubscribe(AbstractGameMessage message) {
            if(message is not AbstractMessage typedMessage) return;
            typedMessage.Event -= cachedDelegate;
        }

        private void OnMessage() {
            broadcast?.Invoke();
        }
    }

}
