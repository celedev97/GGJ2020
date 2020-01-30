using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinkToLevel : MonoBehaviour {
    public string scene_name;
    public string spawnID;

    private void OnTriggerEnter2D(Collider2D collision) {
        SceneManager.LoadScene(scene_name);
        Player.gameObject.transform.position = GameObject.Find(spawnID).transform.position;
    }
}
