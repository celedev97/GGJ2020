using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    public static Dictionary<string, object> variables = new Dictionary<string, object>();

    public static void PlaySound(string path) {
        Debug.Log("SOUND: " + path);
    }

    public static bool AnimatorHasParameter(Animator animator, string paramName) {
        foreach (AnimatorControllerParameter param in animator.parameters) {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    #region Scene loading with teleport
    private static string spawnID;
    public static void loadScene(string sceneName, string spawn_point = "") {
        spawnID = spawn_point;
        Debug.Log("Loading: " + sceneName);
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += TeleportToSpawnPoint;
    }

    private static void TeleportToSpawnPoint(Scene arg0, LoadSceneMode arg1) {
        GameObject spawn = GameObject.Find(spawnID);
        Debug.Log("Teleporting " + Player.gameObject + " to: " + spawnID + "(" + spawn + ")");
        Player.gameObject.transform.position = spawn.transform.position;

        //removing handler for this link
        SceneManager.sceneLoaded -= TeleportToSpawnPoint;
    }
    #endregion

}
