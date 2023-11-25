using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kulibin.Space.MessageBus.Demo {

    [CreateAssetMenu(fileName = "Custom message bus", menuName = "ScriptableObjects/Custom message bus")]

    public class PersistentMessageBus : ScriptableObject {

        public GameMessage gameOver;
        public GameMessageString text;

        public static void GameOver () {
            //gameOver.Invoke();
        }

        public static void Text (string s) {
            //text.Invoke(s);
        }


    }

}