using UnityEngine;

namespace KulibinSpace.MessageBus {

    public delegate void ObjectAction (GameObject g);

    [CreateAssetMenu(fileName = "Object message", menuName = "ScriptableObjects/Object message")]
    public class GameMessageObject : AbstractGameMessage {

        public event ObjectAction message;
        
        public void Invoke (GameObject go) {
            if (message != null) message(go);
        }

    }

}