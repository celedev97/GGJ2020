using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNode;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/BaseDialogGraph", order = 0)]
    public class BaseDialogueGraph : DialogueGraph
    {
        public DialogueFlow flow;
        public UnityEngine.UI.Text gameText;

        public void setColor(int r, int g, int b)
        {
            gameText.color = new Color(r, g, b);
        }

        public void fontSize(int size = 18) {
            gameText.fontSize = size;
        }

        public void loadPrefab(string prefab, bool unload_old_prefabs)
        {
            if (unload_old_prefabs)
            {
                for (int i = 0; i < flow.decorations.transform.childCount; i++)
                {
                    Destroy(flow.decorations.transform.GetChild(i).gameObject);
                }
            }
            Instantiate(Resources.Load(prefab), flow.decorations.transform);
        }

        public void loadScene(string scene, bool additive)
        {
            SceneManager.LoadScene(scene, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }

        public void saveVariable(string variable_name, object value)
        {
            flow.variables[variable_name] = value;
        }

        public object getVariable(string variable_name)
        {
            if (flow.variables.ContainsKey(variable_name))
            {
                return flow.variables[variable_name];
            }
            else
            {
                return null;
            }
        }
    }
}