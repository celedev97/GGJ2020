using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Killable {
    [HideInInspector]
    public new static GameObject gameObject;
    public static Player player;

    public Collider2D sword_hitbox_up;
    public Collider2D sword_hitbox_down;
    public Collider2D sword_hitbox_left;
    public Collider2D sword_hitbox_right;

    private void Start() {
        if (player) {
            Destroy(base.gameObject);
        } else {
            DontDestroyOnLoad(base.gameObject);
            gameObject = base.gameObject;
            player = this;
        }
    }

}
