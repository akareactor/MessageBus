using UnityEngine;

namespace Kulibin.Space.MessageBus { 

    public delegate void EventAction ();

    //[CreateAssetMenu]
    [CreateAssetMenu(fileName = "Message object", menuName = "ScriptableObjects/Message object")]
    public class GameMessage : AbstractGameMessage {

        public event EventAction message;

        public void Invoke () {
            if (message != null) message();
        }

        public void Subscribe (EventAction method) {
            message += method;
        }

        public void Unsubscribe (EventAction method) {
            message -= method;
        }

    }

} 