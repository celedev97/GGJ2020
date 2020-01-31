using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinkToLevel : MonoBehaviour {
    public string scene_name;
    public string spawnID;

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Loading: " + scene_name);
        SceneManager.LoadScene(scene_name);
        SceneManager.sceneLoaded += TeleportToSpawnPoint;
    }

    private void TeleportToSpawnPoint(Scene arg0, LoadSceneMode arg1)
    {
        GameObject spawn = GameObject.Find(spawnID);
        Debug.Log("Teleporting " + Player.gameObject + " to: " + spawnID + "("+ spawn + ")");
        Player.gameObject.transform.position = spawn.transform.position;

        //removing handler for this link
        SceneManager.sceneLoaded -= TeleportToSpawnPoint;
    }
}
