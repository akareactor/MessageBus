using UnityEngine;

namespace Kulibin.Space.MessageBus {

    public delegate void BoolAction (bool flag);

    [CreateAssetMenu(fileName = "Bool message", menuName = "ScriptableObjects/Bool message")]
    public class GameMessageBool : AbstractGameMessage {

        public event BoolAction message;
        
        public void Invoke (bool flag) {
            if (message != null) message(flag);
        }

    }

}