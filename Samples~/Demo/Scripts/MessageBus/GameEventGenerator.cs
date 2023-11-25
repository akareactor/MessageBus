using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kulibin.Space.MessageBus.Demo {

	// ПРОГРАММИРОВАНИЕ БЕЗ ПРОГРАММИРОВАНИЯ
	// GameEventListener слушает события в игре и дёргает соответствующие события у себя. Легко настроить действия в инспекторе.
	// GameEventGenerator позволяет создавать в инспекторе игровые события.
	// 13:19 03.07.2021 архитектура себя зарекомендовала, т.к. очень удобно сооружать генерацию событий прямо по месту

	public class GameEventGenerator : MonoBehaviour {

		public void Log (string s) {
			print("GEG [" + this.GetInstanceID() + "]: " + s);
		}

		public void LevelFailed () { }
		public void Notify (string s) { MessageBus.AddMessage(s); }
		public void MissionRestart () { MessageBus.MissionRestart(); }
		public void MissionExit () { MessageBus.MissionExit(); }
		public void ObjectDestroyed (GameObject go) { MessageBus.Destroyed(go); }
	}

}
