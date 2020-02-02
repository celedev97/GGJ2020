using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroyPlayer : MonoBehaviour
{
    public float reloadTimer = 5;
    // Start is called before the first frame update
    void Start()
    {
        reloadTimer += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > reloadTimer) SceneManager.LoadScene("Splash");
    }
}
