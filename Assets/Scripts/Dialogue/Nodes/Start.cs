using Dialogue;
using XNode;
namespace Dialogue.Functional
{
    [NodeTint("#FFCC55")]
    public class Start : DialogueBaseNode
    {
        [Output] public Connection output;
        
        public override void Trigger()
        {
            goToOutput();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}