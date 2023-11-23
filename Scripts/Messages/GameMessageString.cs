using UnityEngine;

namespace Kulibin.Space.MessageBus {

    public delegate void TextMessage (string s); // простое текстовое сообщение, используется где угодно

    [CreateAssetMenu(fileName = "String message", menuName = "ScriptableObjects/String message")]
    public class GameMessageString : AbstractGameMessage {

        public event TextMessage message;
        
        public void Invoke (string text) {
            if (message != null) message(text);
        }

        public void Subscribe (TextMessage method) {
            message += method;
        }

        public void Unsubscribe (TextMessage method) {
            message -= method;
        }

    }

}