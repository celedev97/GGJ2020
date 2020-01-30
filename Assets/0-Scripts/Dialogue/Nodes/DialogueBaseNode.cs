using System;
using XNode;

namespace Dialogue {
	public abstract class DialogueBaseNode : Node {
		abstract public void Trigger();

		[System.Serializable] public class Connection { }

        protected void goToOutput() {
            (graph as DialogueGraph).current = this;
            (GetOutputPort("output").Connection.node as DialogueBaseNode).Trigger();
        }
    }
}