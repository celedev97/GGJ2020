using Dialogue;
using XNode;
namespace Dialogue.Logical
{
    [NodeTint("#ffbcf3")]
    public class SaveData : DialogueBaseNode
    {
        [Input] public Connection input;
        [Output] public Connection output;

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
                    value = (graph as DialogueGraph).getVariable(this.value);
                }
                else
                {
                    value = this.value;
                }
            }
            switch (operation)
            {
                case Operation.assign:
                    (graph as DialogueGraph).saveVariable(variable_name, value);
                    break;
                case Operation.add:
                    (graph as DialogueGraph).saveVariable(variable_name, "" + (int.Parse((string)(graph as DialogueGraph).getVariable(variable_name)) + int.Parse((string)value)));
                    break;
                case Operation.subtract:
                    (graph as DialogueGraph).saveVariable(variable_name, "" + (int.Parse((string)(graph as DialogueGraph).getVariable(variable_name)) - int.Parse((string)value)));
                    break;
                case Operation.multiply:
                    (graph as DialogueGraph).saveVariable(variable_name, "" + (int.Parse((string)(graph as DialogueGraph).getVariable(variable_name)) * int.Parse((string)value)));
                    break;
                case Operation.divide:
                    (graph as DialogueGraph).saveVariable(variable_name, "" + (int.Parse((string)(graph as DialogueGraph).getVariable(variable_name)) / int.Parse((string)value)));
                    break;
                default:
                    break;
            }
            goToOutput();
        }
        
    }
}