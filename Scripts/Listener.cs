using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace KulibinSpace.MessageBus {

    // Была идея сделать абстрактное поле broadcast и абстрактный Message, чтобы полиморфно подписываться, но пока тупо не знаю, как.
    public abstract class EventItem {}

    /*********************** SIMPLE SIGNAL **********************/

    [System.Serializable]
	public class SignalEvent : UnityEvent {} // добавил для однообразия, чтобы каждому EventItem соответствовал свой подтип UnityEvent

    [Serializable]
    public class SignalEventItem : EventItem {
        [SerializeReference]
        public UnityEvent broadcast = new SignalEvent();
        public void Message () { broadcast?.Invoke(); }
    }

    /*********************** STRING SIGNAL **********************/

    [System.Serializable]
	public class StringEvent : UnityEvent<string> {}

    [Serializable]
    public class StringEventItem : EventItem {
        [SerializeReference]
        public StringEvent broadcast = new();
        public void Message (string s) { broadcast?.Invoke(s); }
    }

    /*********************** INT SIGNAL **********************/

    [System.Serializable]
	public class IntEvent : UnityEvent<int> {}

    [Serializable]
    public class IntEventItem : EventItem {
        [SerializeReference]
        public IntEvent broadcast = new();
        public void Message (int num) { broadcast?.Invoke(num); }
    }

    /*********************** BOOL SIGNAL **********************/

    [System.Serializable]
	public class BoolEvent : UnityEvent<bool> {}

    [Serializable]
    public class BoolEventItem : EventItem {
        [SerializeReference]
        public BoolEvent broadcast = new();
        public void Message (bool b) => broadcast?.Invoke(b);
    }

    /*********************** FLOAT SIGNAL **********************/

    [System.Serializable]
	public class FloatEvent : UnityEvent<float> {}

    [Serializable]
    public class FloatEventItem : EventItem {
        [SerializeReference]
        public FloatEvent broadcast = new();
        public void Message (float f) => broadcast?.Invoke(f);
    }

    /*********************** OBJECT SIGNAL **********************/

    [System.Serializable]
	public class ObjectEvent : UnityEvent<GameObject> {}

    [Serializable]
    public class ObjectEventItem : EventItem {
        [SerializeReference]
        public ObjectEvent broadcast = new();
        public void Message (GameObject go) => broadcast?.Invoke(go);
    }

    /*********************** SCRIPTABLE OBJECT SIGNAL **********************/

    [System.Serializable]
	public class ScriptableObjectEvent : UnityEvent<ScriptableObject> {}

    [Serializable]
    public class ScriptableObjectEventItem : EventItem {
        [SerializeReference]
        public ScriptableObjectEvent broadcast = new();
        public void Message (ScriptableObject so) => broadcast?.Invoke(so);
    }

    /*********************** COMPONENT SIGNAL **********************/

    [System.Serializable]
	public class ComponentEvent : UnityEvent<MonoBehaviour> {}

    [Serializable]
    public class ComponentEventItem : EventItem {
        [SerializeReference]
        public ComponentEvent broadcast = new();
        public void Message (MonoBehaviour mb) => broadcast?.Invoke(mb);
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

        public bool subscribeOnEnable = true;
        private bool subscribed = false;

        public List<Entry> entries {
            get { _entries ??= new List<Entry>(); return _entries; }
            set { _entries = value; }
        }

        // сначала хотел сделать это в редакторе ListenerEditor, но отпугнула сложность рефлексии. Так оказалось проще.
        public void AddEntry (AbstractGameMessage msg) {
            Entry item = new Entry();
            if (msg is GameMessage)
                item.eventItem = new SignalEventItem();
            else if (msg is GameMessageString)
                item.eventItem = new StringEventItem();
            else if (msg is GameMessageInt)
                item.eventItem = new IntEventItem();
            else if (msg is GameMessageFloat)
                item.eventItem = new FloatEventItem();
            else if (msg is GameMessageBool)
                item.eventItem = new BoolEventItem();
            else if (msg is GameMessageObject)
                item.eventItem = new ObjectEventItem();
            else if (msg is GameMessageComponent)
                item.eventItem = new ComponentEventItem();
            else if (msg is GameMessageScriptableObject)
                item.eventItem = new ScriptableObjectEventItem();
            else
                Debug.Log("Cannot detect message type!");
            item.gameMessage = msg;
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
            } else if (msg is GameMessageScriptableObject) {
                (msg as GameMessageScriptableObject).message += (item as ScriptableObjectEventItem).Message;
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
            } else if (msg is GameMessageScriptableObject) {
                (msg as GameMessageScriptableObject).message -= (item as ScriptableObjectEventItem).Message;
            }
        }

        public void Subscribe () {
            if (!subscribed) {
                foreach (Entry item in entries) {
                    Subscribe(item.gameMessage, item.eventItem); // подписать слушателя на делегат сообщения
                }
                subscribed = true;
            }
        }

        public void Unsubscribe () {
            if (subscribed) {
                foreach (Entry item in entries) {
                    Unsubscribe(item.gameMessage, item.eventItem); // отписать слушателя от делегата сообщения
                }
                subscribed = false;
            }
        }

        void OnEnable () {
            if (subscribeOnEnable) Subscribe();
        }

        void OnDisable () {
            Unsubscribe();
        }

    }

}
