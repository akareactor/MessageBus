using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Kulibin.Space.MessageBus {


    public abstract class EventItem {
    }

    [Serializable]
    public class SignalEventItem : EventItem {
        [SerializeReference]
        public UnityEvent broadcast = new UnityEvent();
        public void Message () { if (broadcast != null) broadcast.Invoke(); }
    }

    [System.Serializable]
	public class StringEvent : UnityEvent<string> {}

    [Serializable]
    public class StringEventItem : EventItem {
        [SerializeReference]
        public StringEvent broadcast = new StringEvent();
        public void Message (string s) { if (broadcast != null) broadcast.Invoke(s); }
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
            else
                Debug.Log("Не удалось определить тип!");
            item.gameMessage = gameMessage;
            entries.Add(item);
        }

        void Subscribe (AbstractGameMessage msg, EventItem item) {
            if (msg is GameMessage) {
                (msg as GameMessage).Subscribe((item as SignalEventItem).Message);
            } else if (msg is GameMessageString) {
                (msg as GameMessageString).Subscribe((item as StringEventItem).Message);
            }
        }

        void Unsubscribe (AbstractGameMessage msg, EventItem item) {
            if (msg is GameMessage) {
                (msg as GameMessage).Unsubscribe((item as SignalEventItem).Message);
            } else if (msg is GameMessageString) {
                (msg as GameMessageString).Unsubscribe((item as StringEventItem).Message);
            }
        }


        void OnEnable () {
            // регистрация сообщений в базовой шине, в PlayMode
            foreach (Entry item in entries) {
                Subscribe(item.gameMessage, item.eventItem); // подписать слушателя на делегат сообщения
                Magistral.Register(item.gameMessage); // зарегаться в магистрали (дерегистрацию нельзя делать, т.к. в сцене могут остаться другие)
            }
        }

        void OnDisable () {
            foreach (Entry item in entries) {
                Unsubscribe(item.gameMessage, item.eventItem); // подписать слушателя на делегат сообщения
            }
        }

    }

}