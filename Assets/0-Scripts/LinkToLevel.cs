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
        Debug.Log("Teleporting to: " + spawnID);
        Player.gameObject.transform.position = GameObject.Find(spawnID).transform.position;
    }
}
