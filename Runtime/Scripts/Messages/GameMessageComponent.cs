using UnityEngine;

namespace KulibinSpace.MessageBus {

    [CreateAssetMenu(fileName = "Component message", menuName = "Kulibin Space/MessageBus/Messages/Component message")]
    public class GameMessageComponent : AbstractMessage<MonoBehaviour> {}

}
