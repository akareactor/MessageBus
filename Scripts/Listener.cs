using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor.VersionControl;

namespace Kulibin.Space.MessageBus {

    // Была идея сделать абстрактное поле broadcast и абстрактный Message, чтобы полиморфно подписываться, но пока тупо не знаю, как.
    public abstract class EventItem {}

    /*********************** SIMPLE SIGNAL **********************/

    [System.Serializable]
	public class SignalEvent : UnityEvent {} // добавил для однообразия, чтобы каждому EventItem соответствовал свой подтип UnityEvent

    [Serializable]
    public class SignalEventItem : EventItem {
        [SerializeReference]
        public UnityEvent broadcast = new SignalEvent();
        public void Message () { if (broadcast != null) broadcast.Invoke(); }
    }

    /*********************** STRING SIGNAL **********************/

    [System.Serializable]
	public class StringEvent : UnityEvent<string> {}

    [Serializable]
    public class StringEventItem : EventItem {
        [SerializeReference]
        public StringEvent broadcast = new StringEvent();
        public void Message (string s) { if (broadcast != null) broadcast.Invoke(s); }
    }

    /*********************** INT SIGNAL **********************/

    [System.Serializable]
	public class IntEvent : UnityEvent<int> {}

    [Serializable]
    public class IntEventItem : EventItem {
        [SerializeReference]
        public IntEvent broadcast = new IntEvent();
        public void Message (int num) { if (broadcast != null) broadcast.Invoke(num); }
    }

    /*********************** BOOL SIGNAL **********************/

    [System.Serializable]
	public class BoolEvent : UnityEvent<bool> {}

    [Serializable]
    public class BoolEventItem : EventItem {
        [SerializeReference]
        public BoolEvent broadcast = new BoolEvent();
        public void Message (bool b) { if (broadcast != null) broadcast.Invoke(b); }
    }

    /*********************** FLOAT SIGNAL **********************/

    [System.Serializable]
	public class FloatEvent : UnityEvent<float> {}

    [Serializable]
    public class FloatEventItem : EventItem {
        [SerializeReference]
        public FloatEvent broadcast = new FloatEvent();
        public void Message (float f) { if (broadcast != null) broadcast.Invoke(f); }
    }

    /*********************** OBJECT SIGNAL **********************/

    [System.Serializable]
	public class ObjectEvent : UnityEvent<GameObject> {}

    [Serializable]
    public class ObjectEventItem : EventItem {
        [SerializeReference]
        public ObjectEvent broadcast = new ObjectEvent();
        public void Message (GameObject go) { if (broadcast != null) broadcast.Invoke(go); }
    }

    /*********************** COMPONENT SIGNAL **********************/

    [System.Serializable]
	public class ComponentEvent : UnityEvent<MonoBehaviour> {}

    [Serializable]
    public class ComponentEventItem : EventItem {
        [SerializeReference]
        public ComponentEvent broadcast = new ComponentEvent();
        public void Message (MonoBehaviour mb) { if (broadcast != null) broadcast.Invoke(mb); }
    }

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

        public List<Entry> entries {
            get {
                if (_entries == null) _entries = new List<Entry>();
                return _entries;
            }
            set { _entries = value; }
        }

        // сначала хотел сделать это в редакторе ListenerEditor, но отпугнула сложность рефлексии. Так оказалось проще.
        public void AddEntry (AbstractGameMessage gameMessage) {
            Entry item = new Entry();
            if (gameMessage is GameMessage)
                item.eventItem = new SignalEventItem();
            else if (gameMessage is GameMessageString)
                item.eventItem = new StringEventItem();
            else if (gameMessage is GameMessageInt)
                item.eventItem = new IntEventItem();
            else if (gameMessage is GameMessageFloat)
                item.eventItem = new FloatEventItem();
            else if (gameMessage is GameMessageBool)
                item.eventItem = new BoolEventItem();
            else if (gameMessage is GameMessageObject)
                item.eventItem = new ObjectEventItem();
            else if (gameMessage is GameMessageComponent)
                item.eventItem = new ComponentEventItem();
            else
                Debug.Log("Не удалось определить тип!");
            item.gameMessage = gameMessage;
            entries.Add(item);
        }

        void Subscribe (AbstractGameMessage msg, EventItem item) {
            if (msg is GameMessage) {
                (msg as GameMessage).message += (item as SignalEventItem).Message;
            } else if (msg is GameMessageString) {
                (msg as GameMessageString).message += (item as StringEventItem).Message;
            } else if (msg is GameMessageInt) {
                (msg as GameMessageInt).message += (item as IntEventItem).Message;
            } else if (msg is GameMessageFloat) {
                (msg as GameMessageFloat).message += (item as FloatEventItem).Message;
            } else if (msg is GameMessageBool) {
                (msg as GameMessageBool).message += (item as BoolEventItem).Message;
            } else if (msg is GameMessageObject) {
                (msg as GameMessageObject).message += (item as ObjectEventItem).Message;
            } else if (msg is GameMessageComponent) {
                (msg as GameMessageComponent).message += (item as ComponentEventItem).Message;
            }
        }

        void Unsubscribe (AbstractGameMessage msg, EventItem item) {
            if (msg is GameMessage) {
                (msg as GameMessage).message -= (item as SignalEventItem).Message;
            } else if (msg is GameMessageString) {
                (msg as GameMessageString).message -= (item as StringEventItem).Message;
            } else if (msg is GameMessageInt) {
                (msg as GameMessageInt).message -= (item as IntEventItem).Message;
            } else if (msg is GameMessageFloat) {
                (msg as GameMessageFloat).message -= (item as FloatEventItem).Message;
            } else if (msg is GameMessageBool) {
                (msg as GameMessageBool).message -= (item as BoolEventItem).Message;
            } else if (msg is GameMessageObject) {
                (msg as GameMessageObject).message -= (item as ObjectEventItem).Message;
            } else if (msg is GameMessageComponent) {
                (msg as GameMessageComponent).message -= (item as ComponentEventItem).Message;
            }
        }

        void OnEnable () {
            // регистрация сообщений в базовой шине, в PlayMode
            foreach (Entry item in entries) {
                Subscribe(item.gameMessage, item.eventItem); // подписать слушателя на делегат сообщения
            }
        }

        void OnDisable () {
            foreach (Entry item in entries) {
                Unsubscribe(item.gameMessage, item.eventItem); // подписать слушателя на делегат сообщения
            }
        }

    }

}