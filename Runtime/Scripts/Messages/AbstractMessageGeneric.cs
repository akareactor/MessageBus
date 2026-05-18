using System;

namespace KulibinSpace.MessageBus {

    public interface IMessage<T> {
        void Invoke (T message);
    }

    public abstract class AbstractMessage<T> : AbstractGameMessage, IMessage<T> {

        public event Action<T> Event;

        public event Action<T> message {
            add => Event += value;
            remove => Event -= value;
        }

        public virtual void Invoke (T message) {
            Event?.Invoke(message);
        }
    }

}


