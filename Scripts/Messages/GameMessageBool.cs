using UnityEngine;

namespace KulibinSpace.MessageBus {

    public delegate void BoolAction (bool flag);

    [CreateAssetMenu(fileName = "Bool message", menuName = "Kulibin Space/MessageBus/Messages/Bool message")]
    public class GameMessageBool : AbstractGameMessage {

        public event BoolAction message;
        
        public void Invoke (bool flag) {
            if (message != null) message(flag);
        }

    }

}
