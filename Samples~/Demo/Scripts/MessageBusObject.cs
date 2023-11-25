using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kulibin.Space.MessageBus.Demo {

    // декорация для прямого вызова

    public class MessageBusObject : MonoBehaviour {

        public List<AbstractGameMessage> messages;

        AbstractGameMessage Find (string messageName) { return messages.Find(x => x.name == messageName); }

        // однократная регистрация в глобальной магистрали
        void Awake () {
            foreach (AbstractGameMessage item in messages) {
                Magistral.Register(item);
            }
        }

        public void GameOver () {
            AbstractGameMessage agm = Find("GameOver");
            if (agm != null && agm is GameMessage) ((GameMessage)agm).Invoke();
        }

        public void TextMessage (string s) {
            AbstractGameMessage agm = Find("Text");
            if (agm != null && agm is GameMessageString) ((GameMessageString)agm).Invoke(s);
        }

    }

}

