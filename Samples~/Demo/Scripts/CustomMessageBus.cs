using UnityEngine;

namespace KulibinSpace.MessageBus.Demo {

    // Place it on the scene
    // Предметный глобальный клиентский широковещатель. Реализуется на стороне клиента, потому что сообщения идентифицируются по имени скриптуемого объекта, а это вне ответственности компилятора.
    // Поэтому в общем случае клиент должен самостоятельно внедрять сообщения в собственный код.

    public class CustomMessageBus : MonoBehaviour {

        static CustomMessageBus instance;
        public GameMessage gameOver;
        public GameMessageString text;
        public GameMessageInt progress;
        public GameMessageFloat number;
        public GameMessageBool flag;
        public GameMessageObject gobj;
        public GameMessageComponent component;

        void Awake () {
            instance = this;
        }

        public static void GameOver () {
            if (instance) instance.gameOver?.Invoke();
        }

        public static void Text (string s) {
            if (instance) instance.text?.Invoke(s);
        }

        public static void Progress (int val) {
            if (instance) instance.progress?.Invoke(val);
        }

        public static void Number (float val) {
            if (instance) instance.number?.Invoke(val);
        }

        public static void Flag (bool val) {
            if (instance) instance.flag?.Invoke(val);
        }

        public static void Object (GameObject go) {
            if (instance) instance.gobj?.Invoke(go);
        }

        public static void Component (MonoBehaviour component) {
            if (instance) instance.component?.Invoke(component);
        }

    }

}