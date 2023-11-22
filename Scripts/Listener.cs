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

    [Serializable]
    public class StringEventItem : EventItem {
        [SerializeReference]
        public StringEvent broadcast = new StringEvent();
        public void Message (string s) { if (broadcast != null) broadcast.Invoke(s); }
    }

    // связь слушателя и сообщения
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

        void Awake () {
            // регистрация сообщений в базовой шине
            foreach (Entry item in entries) {
                //item.gameMessage.Subscribe(); //item.gameMessage.message += item.Message; // подписка на сообщение
                //Magistral.Register(item.gameMessage);
            }
        }

    }

}