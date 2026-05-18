using UnityEngine;

namespace KulibinSpace.MessageBus {

    [CreateAssetMenu(fileName = "Object message", menuName = "Kulibin Space/MessageBus/Messages/Scriptable Object message")]
    public class GameMessageScriptableObject : AbstractMessage<ScriptableObject> {}

}
