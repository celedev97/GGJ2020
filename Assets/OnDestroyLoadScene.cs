using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDestroyLoadScene : MonoBehaviour
{
    public string scene_name;
    public string spawnID;

    private void OnDestroy() {
        Game.loadScene(scene_name, spawnID);
    }

}
