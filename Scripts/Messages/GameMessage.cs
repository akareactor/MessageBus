using UnityEngine;

namespace KulibinSpace.MessageBus { 

    public delegate void EventAction ();

    [CreateAssetMenu(fileName = "Message object", menuName = "Kulibin Space/Scriptable Objects/Messages/Message object")]
    public class GameMessage : AbstractGameMessage {

        public event EventAction message;
        
        public void Invoke () {
            if (message != null) message();
        }

    }

}
