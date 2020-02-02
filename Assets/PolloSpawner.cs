using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloSpawner : MonoBehaviour
{
    public GameObject polloPrefab;

    public float pollosPerSecond = 1;

    public float nextPolloSpawnTime;

    public float speedVariability = 2;
    public float pitchVariability = 1.5f;

    public void Update() {
        if (Time.time > nextPolloSpawnTime) {
            //setting the timer again
            nextPolloSpawnTime = Time.time + 1f / pollosPerSecond;

            //picking a random spawnpoint for the cucco
            Vector3 spawn = transform.GetChild(Random.Range(0, transform.childCount)).position;

            //spawning the cucco
            GameObject cucco = GameObject.Instantiate(polloPrefab, spawn, Quaternion.identity);
            Vector3 direction = Player.gameObject.transform.position - spawn;
            cucco.GetComponent<Projectile>().direction = direction.normalized;

            //variating speed
            cucco.GetComponent<Projectile>().speed += Random.Range(-speedVariability, speedVariability);

            //variating pitch
            cucco.GetComponent<AudioSource>().pitch += Random.Range(-pitchVariability, pitchVariability);

            //flipping it in case it's not facing the player
            if (direction.x > 0) {
                cucco.transform.localScale = new Vector3(-1, 1, 1);
            }

        }
    }
}
