using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KulibinSpace.MessageBus;

// all basic types
// просто дёрнуть за метод

public class GameMessageActuator : MonoBehaviour {

    public GameMessage gameMessage;
    public GameMessageBool gameMessageBool;
    public GameMessageComponent gameMessageComponent;
    public GameMessageFloat gameMessageFloat;
    public GameMessageInt gameMessageInt;
    public GameMessageObject gameMessageObject;
    public GameMessageString gameMessageString;

    public void GameMessageActuate () => gameMessage.Invoke();
    public void GameMessageBoolActuate (bool par) => gameMessageBool.Invoke(par);
    public void GameMessageComponentActuate (MonoBehaviour par) => gameMessageComponent.Invoke(par);
    public void GameMessageFloatActuate (float par) => gameMessageFloat.Invoke(par);
    public void GameMessageIntActuate (int par) => gameMessageInt.Invoke(par);
    public void GameMessageObjectActuate (GameObject par) => gameMessageObject.Invoke(par);
    public void GameMessageObjectString (string par) => gameMessageString.Invoke(par);

}
