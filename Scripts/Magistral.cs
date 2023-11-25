using System.Collections.Generic;

namespace Kulibin.Space.MessageBus {

    public static class Magistral {

        private static List<AbstractGameMessage> _messages;

        private static List<AbstractGameMessage> messages {
            get { if (_messages == null) _messages = new List<AbstractGameMessage>(); return _messages; }
            set { _messages = value; }
        }

        public static void Register (AbstractGameMessage gms) {
            if (!messages.Contains(gms)) messages.Add(gms); 
        }

        static AbstractGameMessage Find (string messageName) {
        return messages.Find(x => x.name == messageName);
        }

        public static void InvokeMessageByName (string messageName) {
            AbstractGameMessage agm = Find(messageName);
            if (agm != null && agm is GameMessage) ((GameMessage)agm).Invoke();
        }

        public static void InvokeMessageByName (string messageName, string text) {
            AbstractGameMessage agm = Find(messageName);
            if (agm != null && agm is GameMessageString) ((GameMessageString)agm).Invoke(text);
        }
    }

}