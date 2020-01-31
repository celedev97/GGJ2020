using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public new static GameObject gameObject;
    public static Player player;

    private void Start() {
        //initializing public static things, so i can access the player from every other object without the need to use GetComponent<>
        if (player)
        {
            Destroy(base.gameObject);
        }
        else {
            gameObject = base.gameObject;
            player = this;
        }
    }



}
