using UnityEngine;

namespace KulibinSpace.MessageBus {

    [CreateAssetMenu(fileName = "Object message", menuName = "Kulibin Space/MessageBus/Messages/Object message")]
    public class GameMessageObject : AbstractMessage<GameObject> {}

}
