using UnityEngine;

namespace Kulibin.Space.MessageBus {

    public delegate void IntAction (int val);

    [CreateAssetMenu(fileName = "Int message", menuName = "ScriptableObjects/Int message")]
    public class GameMessageInt : AbstractGameMessage {

        public event IntAction message;
        
        public void Invoke (int val) {
            if (message != null) message(val);
        }

    }

}