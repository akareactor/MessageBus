using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Reflection;

namespace Kulibin.Space.MessageBus {

	// ПРИМЕР ТИПИЗИРОВАННОГО СООБЩЕНИЯ
	// common - обычное сообщение, выводится, например, справа вверху в небольшом списке
	// urgent - настоятельное сообщение (о повреждении), выводитс¤ по центру с анимацией
	public enum MessageType { Common, Urgent }

	public struct TypedMessage {
		public MessageType type;
		public string s;
		public TypedMessage (MessageType type, string s) {
			this.type = type;
			this.s = s;
		}
	}

// по каждому делегату в скрипт сообщения
	public delegate void ObjectAction (GameObject g);
	public delegate void ComponentAction (MonoBehaviour mb);
	public delegate void BoolAction (bool flag); // 19:29 29.08.2021 понадобилось дл¤ делегата PlayerController.OnSniperMode (там вход и выход, экономи¤)
	public delegate void FloatAction (float val);
	public delegate void AdvancedMessage (TypedMessage m);

// по каждому событию в слушателя
    [System.Serializable]
	public class IntEvent : UnityEvent<int> {}

    [System.Serializable]
	public class BoolEvent : UnityEvent<bool> {}

    [System.Serializable]
	public class FloatEvent : UnityEvent<float> {}

    [System.Serializable]
	public class ObjectEvent : UnityEvent<GameObject> {}

    public abstract class AbstractGameMessage : ScriptableObject {

        public static AbstractGameMessage[] GetAllInstances () {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(AbstractGameMessage).Name); //FindAssets uses tags check documentation for more info
            AbstractGameMessage[] a = new AbstractGameMessage[guids.Length];
            for (int i = 0; i < guids.Length; i++) {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = (AbstractGameMessage)AssetDatabase.LoadAssetAtPath(path, typeof(AbstractGameMessage));
            }
        return a;
        }

    }

} 