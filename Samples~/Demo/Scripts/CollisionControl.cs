using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace KulibinSpace.MessageBus.Demo {

	public class CollisionControl : MonoBehaviour {

		public GameMessage gms;

		private void OnCollisionEnter (Collision collision) {
			print("Collision moment message sent to bus!");
			//MessageBus.AddMessage("Collision!");
			gms.Invoke();
		}

	}

}
