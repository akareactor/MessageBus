using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kulibin.Space.MessageBus {

	public class CollisionControl : MonoBehaviour {

		private void OnCollisionEnter (Collision collision) {
			print("Collision moment message added!");
			MessageBus.AddMessage("Collision!");
		}

	}

}
