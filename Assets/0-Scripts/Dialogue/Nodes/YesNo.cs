using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue.Logical{
    [NodeTint("#26e5ef")]
    public class YesNo : Dialogue.Text.Text {
        [Output] public Connection yes;
        [Output] public Connection no;

        public void Trigger(bool answer) {
            if (answer)
            {
                (GetOutputPort("yes").Connection.node as DialogueBaseNode).Trigger();
            }
            else {
                (GetOutputPort("no").Connection.node as DialogueBaseNode).Trigger();
            }
            
        }

        public override void Trigger() {
            (graph as DialogueGraph).current = this;
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "yes")
            {
                return true;
            }
            else if (port.fieldName == "no")
            {
                return false;
            }
            return null;
        }
    }
}