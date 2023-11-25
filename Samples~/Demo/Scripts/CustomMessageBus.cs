using UnityEngine;

namespace Kulibin.Space.MessageBus.Demo {

    // Place it on the scene
    // Предметный глобальный клиентский широковещатель. Реализуется на стороне клиента, потому что сообщения идентифицируются по имени скриптуемого объекта, а это вне ответственности компилятора.
    // Поэтому в общем случае клиент должен самостоятельно внедрять сообщения в собственный код.
    // Есть ещё магистраль, которая может выбирать предметные сообщения по имени скриптуемого объекта.

    public class CustomMessageBus : MonoBehaviour {

        static CustomMessageBus instance;
        public GameMessage gameOver;
        public GameMessageString text;

        void Awake () {
            instance = this;
        }

        public static void GameOver () {
            if (instance) instance.gameOver?.Invoke();
        }

        public static void Text (string s) {
            if (instance) instance.text?.Invoke(s);
        }

    }

}