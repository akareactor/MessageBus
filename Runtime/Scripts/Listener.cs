using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace KulibinSpace.MessageBus {

    [Serializable] public class StringEventItem : EventItem<string> { }
    [Serializable] public class IntEventItem : EventItem<int> { }
    [Serializable] public class BoolEventItem : EventItem<bool> { }
    [Serializable] public class FloatEventItem : EventItem<float> { }
    [Serializable] public class ScriptableObjectEventItem : EventItem<ScriptableObject> { }
    [Serializable] public class ComponentEventItem : EventItem<MonoBehaviour> { }
    [Serializable] public class ObjectEventItem : EventItem<GameObject> { }

    // связь слушателя и сообщения надо делать в отдельном классе, т.к. Editor не работает с абстрактным элементом EventItem
    [Serializable]
    public class Entry {
        [SerializeReference]
        public EventItem eventItem;
        public AbstractGameMessage gameMessage; // по конкретному типу выбирается соответствующий подкласс EventItem
    }

    // Placing on scene
    public class Listener : MonoBehaviour {

        [SerializeField]
        private List<Entry> _entries;

        public bool subscribeOnEnable = true;
        private bool subscribed = false;

        public List<Entry> entries {
            get { _entries ??= new List<Entry>(); return _entries; }
            set { _entries = value; }
        }


#if UNITY_EDITOR
        public void AddEntry (AbstractGameMessage msg) {
            Entry item = new();

            Type type = msg.GetType();

            while (type != null) {

                // Payload message
                if (type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(AbstractMessage<>)) {

                    Type payloadType = type.GetGenericArguments()[0];
                    item.eventItem = EventItemRegistry.Create(payloadType);
                    break;
                }

                // Void message
                if (type == typeof(AbstractMessage)) {
                    item.eventItem = new VoidEventItem();
                    break;
                }

                type = type.BaseType;
            }

            if (item.eventItem == null) {
                Debug.LogError($"Cannot create EventItem for {msg.GetType()}");
                return;
            }

            item.gameMessage = msg;
            entries.Add(item);
        }
#endif

        public void Subscribe () {
            if (subscribed) return;
            foreach (var item in entries) item.eventItem.Subscribe(item.gameMessage);
            subscribed = true;
        }

        public void Unsubscribe () {
            if (!subscribed) return;
            foreach (var item in entries) item.eventItem.Unsubscribe(item.gameMessage);
            subscribed = false;
        }

        void OnEnable () {
            if (subscribeOnEnable) Subscribe();
        }

        void OnDisable () {
            Unsubscribe();
        }

    }

}
