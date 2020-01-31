using Dialogue;
using XNode;

namespace Dialogue.Functional
{
    public class Flow : DialogueBaseNode {

        public override void Trigger() {
            (GetOutputPort("output").Connection.node as DialogueBaseNode).Trigger();
        }
    }
}