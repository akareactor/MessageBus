using System.Collections;
using System.Collections.Generic;
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

	}

}

