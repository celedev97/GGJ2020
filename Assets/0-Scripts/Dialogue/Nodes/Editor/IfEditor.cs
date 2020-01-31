using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(Dialogue.Logical.If))]
    public class IfEditor : NodeEditor {
        public override int GetWidth() {
            return 336;
        }
    }
}