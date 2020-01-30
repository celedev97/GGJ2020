using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue.Text {
    [NodeTint("#CCFFCC")]
    public class Choice : Text {
        public List<Answer> answers = new List<Answer>();
        public string afterText;

        [System.Serializable]
        public class Answer
        {
            public string text;
        }

        public override void Trigger(string indexString) {
            int index = int.Parse(indexString)-1;
            NodePort outputPort = null;
            if (answers.Count == 0) {
                outputPort = GetOutputPort("output");
            } else {
                if (answers.Count <= index) return;
                outputPort = GetOutputPort("answers " + index);
            }

            if (outputPort != null)
            {
                (outputPort.Connection.node as DialogueBaseNode).Trigger();
            }
        }
        
    }
}