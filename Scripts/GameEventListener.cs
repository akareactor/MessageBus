using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kulibin.Space.MessageBus {

[System.Serializable]
	public class IntEvent : UnityEvent<int> {}

[System.Serializable]
	public class BoolEvent : UnityEvent<bool> {}

[System.Serializable]
	public class StringEvent : UnityEvent<string> {}

[System.Serializable]
	public class FloatEvent : UnityEvent<float> {}

[System.Serializable]
	public class ObjectEvent : UnityEvent<GameObject> {}

	public class GameEventListener : MonoBehaviour {

		public UnityEvent onGameOver;
		public UnityEvent onGameStart;
		public UnityEvent onGameContinued;
		public StringEvent onMessage;

		void OnEnable() {
			MessageBus.OnGameOver += GameOver;
			MessageBus.OnGameStart += GameStart;
			MessageBus.OnGameContinued += GameContinued;
			MessageBus.OnTextMessage += Message;
		}
	
		void OnDisable() {
			MessageBus.OnGameOver -= GameOver;
			MessageBus.OnGameStart -= GameStart;
			MessageBus.OnGameContinued -= GameContinued;
			MessageBus.OnTextMessage -= Message;
		}

		void GameOver () { if (onGameOver != null) onGameOver.Invoke(); }
		void GameStart () { if (onGameStart != null) onGameStart.Invoke(); }
		void GameContinued () { if (onGameContinued != null) onGameContinued.Invoke(); }
		void Message(string s) { if (onMessage != null) onMessage.Invoke(s); }
	}
}
