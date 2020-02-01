using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnChildDeath : MonoBehaviour
{
    public string sceneName;
    public string spawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0) {
            Game.loadScene(sceneName, spawnPoint);
        }
    }
}
