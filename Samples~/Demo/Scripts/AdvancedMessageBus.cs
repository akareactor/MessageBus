using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kulibin.Space.MessageBus.Demo {

    // декорация для прямого вызова

    public class AdvancedMessageBus : MonoBehaviour {

        public void GameOver () {
            Magistral.InvokeMessageByName("GameOver");
        }

    }

}

