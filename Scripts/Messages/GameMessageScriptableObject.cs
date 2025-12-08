using UnityEngine;

namespace KulibinSpace.MessageBus {

    public delegate void ScriptableObjectAction (ScriptableObject so);

    [CreateAssetMenu(fileName = "Object message", menuName = "Kulibin Space/MessageBus/Messages/Scriptable Object message")]
    public class GameMessageScriptableObject : AbstractGameMessage {
        public event ScriptableObjectAction message;
        public void Invoke (ScriptableObject so) { if (message != null) message(so); }
    }

}
