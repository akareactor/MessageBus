using System.Collections;
using System.Collections.Generic;
using KulibinSpace.MessageBus.Demo;
using UnityEngine;

namespace KulibinSpace.MessageBusDemo {

	public class SimpleLog : MonoBehaviour {

		public void Write (string s) {
			print(s);
		}

        public void Write (ScriptableObject so) {
            if (so != null && so is ScriptableContainer sc) {
                print(so.name + ", " + sc.content);
            }
        }

        public void Write (Hit hit) {
            print(hit.damage);
        }

	}

}

