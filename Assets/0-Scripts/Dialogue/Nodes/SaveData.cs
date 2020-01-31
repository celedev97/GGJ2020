using Dialogue;
using XNode;
namespace Dialogue.Logical
{
    [NodeTint("#ffbcf3")]
    public class SaveData : DialogueBaseNode
    {

        public bool node_input = false;

        public bool value_is_var;

        public string value;

        public string variable_name;

        public Operation operation;

        public enum Operation {
            assign,
            add,
            subtract,
            multiply,
            divide,
        }

        public override void Trigger()
        {
            object value = "";
            if (node_input)
            {
                NodePort nodePort = GetInputPort("input");
                value = (string)nodePort.Connection.node.GetValue(nodePort.Connection.node.GetOutputPort("output"));
            }
            else {
                if (value_is_var)
                {
                    value = Game.GetVariable(this.value);
                }
                else
                {
                    value = this.value;
                }
            }
            switch (operation)
            {
                case Operation.assign:
                    Game.SaveVariable(variable_name, value);
                    break;
                case Operation.add:
                    Game.SaveVariable(variable_name, "" + (int.Parse((string)Game.GetVariable(variable_name)) + int.Parse((string)value)));
                    break;
                case Operation.subtract:
                    Game.SaveVariable(variable_name, "" + (int.Parse((string)Game.GetVariable(variable_name)) - int.Parse((string)value)));
                    break;
                case Operation.multiply:
                    Game.SaveVariable(variable_name, "" + (int.Parse((string)Game.GetVariable(variable_name)) * int.Parse((string)value)));
                    break;
                case Operation.divide:
                    Game.SaveVariable(variable_name, "" + (int.Parse((string)Game.GetVariable(variable_name)) / int.Parse((string)value)));
                    break;
                default:
                    break;
            }
            (GetOutputPort("output").Connection.node as DialogueBaseNode).Trigger();
        }
        
    }
}