using UnityEngine;
using System.Collections;
using System;

// Глобальна¤ шина сообщений
namespace Kulibin.Space.MessageBus {
	
	public static class MessageBus {

		public static event EventAction OnGameOver;
		public static event EventAction OnGameStart;
		public static event EventAction OnGameContinued;
		// 
		public static event EventAction OnRestartMission;
		public static event EventAction OnExitMission;
		public static event ObjectAction OnDestroy;
		public static event TextMessage OnTextMessage;
		public static event AdvancedMessage OnMessage;
		public static event BoolAction OnLoadingProgress;

		// Color Lines
		public static void GameOver () { if (OnGameOver != null) OnGameOver(); }
		public static void GameStart () { if (OnGameStart != null) OnGameStart(); }
		public static void GameContinued () { if (OnGameContinued != null) OnGameContinued(); }

		// послать сообщение кому-то, кто покажет индикатор загрузки.
		public static void LoadingProgress (bool on) { if (OnLoadingProgress != null) OnLoadingProgress(on); }
		public static void MissionRestart () { if (OnRestartMission != null) OnRestartMission(); }
		public static void MissionExit () { if (OnExitMission != null) OnExitMission(); }
		public static void Destroyed (GameObject g) { if (OnDestroy != null) OnDestroy(g);	}
		public static void AddMessage (TypedMessage m) { if (OnMessage != null) OnMessage(m); }
		public static void AddMessage (string s) { if (OnTextMessage != null) OnTextMessage(s); }

	}

}