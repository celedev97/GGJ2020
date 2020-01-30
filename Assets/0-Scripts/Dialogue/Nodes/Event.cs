using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Unity;
using UnityEngine.Events;

namespace Dialogue.Functional
{
	[NodeTint("#FFFFAA")]
	public class Event : DialogueBaseNode {
        [Input] public Connection input;
        [Output] public Connection output;

        public UnityEvent[] trigger;

		public override void Trigger() {
			for (int i = 0; i < trigger.Length; i++) {
				trigger[i].Invoke();
			}
            goToOutput();
        }

	}
}