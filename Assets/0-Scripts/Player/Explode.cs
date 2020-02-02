using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
    public float explodeAfterSeconds = 1;
    public float deleteAfterSeconds = 1;

    private float explosionTimer = 0;
    private float deleteTimer = 0;

    // Start is called before the first frame update
    void Start() {
        explosionTimer = Time.time + explodeAfterSeconds;
        deleteTimer = Time.time + explodeAfterSeconds + deleteAfterSeconds;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > explosionTimer) {
            explosionTimer = float.MaxValue;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<AudioSource>().Play();
        }
        if (Time.time > deleteTimer) {
            Destroy(gameObject);
            this.enabled = false;
        }
    }
}
