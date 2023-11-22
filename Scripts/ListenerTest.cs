using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Kulibin.Space.MessageBus {

    [Serializable]
    public class EntryTest {
        public AbstractGameMessage gameMessage;
        public StringEvent broadcast;
        //public void Message (string s) { if (broadcast != null) broadcast.Invoke(s); }
    }

    public class ListenerTest : MonoBehaviour {

        public List<EntryTest> entries;

    }

}