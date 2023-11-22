using UnityEngine;

namespace Kulibin.Space.MessageBus {

    [CreateAssetMenu(fileName = "String message", menuName = "ScriptableObjects/String message")]
    public class GameMessageString : AbstractGameMessage {

        public event TextMessage message;
        
        public void Invoke (string text) {
            if (message != null) message(text);
        }

        public override void Subscribe () {
            
        }

    }

}