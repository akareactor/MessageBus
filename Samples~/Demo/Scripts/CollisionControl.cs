using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace KulibinSpace.MessageBus.Demo {

	public class CollisionControl : MonoBehaviour {

		public GameMessage gms;
        public HitMessage hitMsg;

		private void OnCollisionEnter (Collision collision) {
			print("Collision moment message sent to bus!");
			//MessageBus.AddMessage("Collision!");
			gms.Invoke();
            Hit hit = new Hit();
            hit.damage = collision.impulse.magnitude;
            hitMsg.Invoke(hit);
		}

	}

}
