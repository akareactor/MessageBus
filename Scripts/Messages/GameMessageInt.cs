using UnityEngine;

namespace KulibinSpace.MessageBus {

    public delegate void IntAction (int val);

    [CreateAssetMenu(fileName = "Int message", menuName = "Kulibin Space/MessageBus/Messages/Int message")]
    public class GameMessageInt : AbstractGameMessage {

        public event IntAction message;
        
        public void Invoke (int val) {
            if (message != null) message(val);
        }

    }

}
