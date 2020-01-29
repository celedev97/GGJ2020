using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue.Text {
    [NodeTint("#26e5ef")]
    public class Input : Text {
        private string user_input;

        public override void Trigger(string input) {
            user_input = input;
            goToOutput();
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "output")
            {
                return user_input;
            }
            return null;
        }

    }
}