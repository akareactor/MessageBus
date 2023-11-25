using UnityEngine;

namespace Kulibin.Space.MessageBus { 

    public delegate void EventAction ();

    [CreateAssetMenu(fileName = "Message object", menuName = "ScriptableObjects/Message object")]
    public class GameMessage : AbstractGameMessage {

        public event EventAction message;

        public void Invoke () {
            if (message != null) message();
        }

    }

} 