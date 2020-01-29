using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Dialogue.Logical {
    [CustomNodeEditor(typeof(YesNo))]
    public class ChoiceEditor : NodeEditor {

        public override void OnBodyGUI() {
            YesNo dialogue = target as YesNo;
            
            NodeEditorGUILayout.PortField(target.GetInputPort("input"));
            NodeEditorGUILayout.PortField(target.GetOutputPort("yes"));
            NodeEditorGUILayout.PortField(target.GetOutputPort("no"));

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("dont_clear"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("text"));
        }
        
    }
}