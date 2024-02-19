using UnityEngine;

namespace KulibinSpace.MessageBus {

    public delegate void ComponentAction (MonoBehaviour mb);

    [CreateAssetMenu(fileName = "Component message", menuName = "ScriptableObjects/Component message")]
    public class GameMessageComponent : AbstractGameMessage {

        public event ComponentAction message;
        
        public void Invoke (MonoBehaviour mb) {
            if (message != null) message(mb);
        }

    }

}