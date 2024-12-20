using UnityEngine;

namespace KulibinSpace.MessageBus {

    public delegate void StringAction (string s); // простое текстовое сообщение, используется где угодно

    [CreateAssetMenu(fileName = "String message", menuName = "Kulibin Space/Scriptable Objects/Messages/String message")]
    public class GameMessageString : AbstractGameMessage {

        public event StringAction message;
        
        public void Invoke (string s) {
            if (message != null) message(s);
        }

    }

}
