namespace KulibinSpace.MessageBus {
 
    public interface IMessage <T> {
        public void Invoke (T message);
    }

}
