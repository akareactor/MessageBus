using System;

namespace KulibinSpace.MessageBus {

    public interface IMessage {
        void Invoke();
    }

    public abstract class AbstractMessage : AbstractGameMessage, IMessage {

        public event Action Event;

        public virtual void Invoke() {
            Event?.Invoke();
        }
    }

}
