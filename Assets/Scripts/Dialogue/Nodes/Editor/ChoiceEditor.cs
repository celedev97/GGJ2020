using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Dialogue.Text {
    [CustomNodeEditor(typeof(Choice))]
    public class ChoiceEditor : NodeEditor {

        public override void OnBodyGUI() {
            Choice dialogue = target as Choice;
            GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            EditorGUILayout.Space();
            if (dialogue.answers.Count == 0) NodeEditorGUILayout.PortField(target.GetOutputPort("output"), GUILayout.Width(100));
            GUILayout.EndHorizontal();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("text"));
            NodeEditorGUILayout.InstancePortList("answers", typeof(string), serializedObject, NodePort.IO.Output, Node.ConnectionType.Override);
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("afterText"));
        }

        public override int GetWidth() {
            return 400;
        }
    }
}