using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using XNode;

namespace Dialogue.Logical
{
    [NodeTint("#CCCCFF")]
    public class If : DialogueBaseNode
    {
        [Output] public DialogueBaseNode pass;
        [Output] public DialogueBaseNode fail;
        public Condition[] conditions;

        public override void Trigger()
        {
            // Perform condition
            bool pass = false;
            foreach (Condition condition in conditions) {
                string value1 = (string)Game.GetVariable(condition.variable1);
                string value2 = condition.var_2_is_value ? condition.variable2 : (string)Game.GetVariable(condition.variable2);

                switch (condition.operation)
                {
                    case Condition.Operation.equal:
                        if (value1 == value2) pass = true;
                        break;
                    case Condition.Operation.more_than:
                        if (int.Parse(value1) > int.Parse(value2)) pass = true;
                        break;
                    case Condition.Operation.more_eq_than:
                        if (int.Parse(value1) >= int.Parse(value2)) pass = true;
                        break;
                    case Condition.Operation.less_than:
                        if (int.Parse(value1) < int.Parse(value2)) pass = true;
                        break;
                    case Condition.Operation.less_eq_than:
                        if (int.Parse(value1) <= int.Parse(value2)) pass = true;
                        break;
                    case Condition.Operation.contains:
                        if (value1.Contains(value2)) pass = true;
                        break;
                    default:
                        break;
                }

                if (pass) break;
            }

            //Trigger next nodes
            NodePort port;

            if (pass) port = GetOutputPort("pass");
            else port = GetOutputPort("fail");

            if (port == null) return;

            for (int i = 0; i < port.ConnectionCount; i++)
            {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }
        }

    }

    [Serializable]
    public class Condition {
        public enum Operation {
            equal,
            more_than,
            more_eq_than,
            less_than,
            less_eq_than,
            contains,
        }
        public Operation operation;
        public string variable1;
        public bool var_2_is_value;
        public string variable2;
    }
}