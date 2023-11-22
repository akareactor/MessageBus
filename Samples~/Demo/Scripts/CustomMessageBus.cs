using UnityEngine;

namespace Kulibin.Space.MessageBus.Demo {

    // Place it on the scene

    public class CustomMessageBus : MonoBehaviour {

        static CustomMessageBus instance;
        public GameMessage gameOver;

        void Awake () {
            instance = this;
        }

        public static void GameOver () {
            if (instance.gameOver) instance.gameOver.Invoke();
        }

    }

}