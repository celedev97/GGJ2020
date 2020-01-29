using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Dialogue.Logical {
    [CustomNodeEditor(typeof(SaveData))]
    public class SaveDataEditor : NodeEditor {

        public override void OnBodyGUI() {
            SaveData dialogue = target as SaveData;
            GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            EditorGUILayout.Space();
            NodeEditorGUILayout.PortField(target.GetOutputPort("output"), GUILayout.Width(100));
            GUILayout.EndHorizontal();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("node_input"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("variable_name"));
            if (!serializedObject.FindProperty("node_input").boolValue) {
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("value_is_var"));
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
            }
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("operation"));
        }

        public override int GetWidth()
        {
            return 275;
        }

    }
}