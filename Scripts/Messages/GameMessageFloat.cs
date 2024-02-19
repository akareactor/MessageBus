using UnityEngine;

namespace KulibinSpace.MessageBus {

    public delegate void FloatAction (float val);

    [CreateAssetMenu(fileName = "Float message", menuName = "ScriptableObjects/Float message")]
    public class GameMessageFloat : AbstractGameMessage {

        public event FloatAction message;
        
        public void Invoke (float val) {
            if (message != null) message(val);
        }

    }

}