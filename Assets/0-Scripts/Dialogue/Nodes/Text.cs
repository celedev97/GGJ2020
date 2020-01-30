using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue.Text {
    [NodeTint("#CCFFCC")]
    public class Text : DialogueBaseNode {
        [Input] public Connection input;
        [Output] public Connection output;
        public bool dont_clear;
        [TextArea] public string text;
        public Alteration textAlteration;

        public enum Alteration
        {
            none,
            angry,
            scared,
            happy,
            confused,
        }

        public virtual void Trigger(string input) {
            goToOutput();
        }

        public override void Trigger() {
            (graph as DialogueGraph).current = this;
        }
    }
}