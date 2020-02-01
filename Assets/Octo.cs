using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octo : MonoBehaviour {
    public float shootDistance = .5f;
    public float shootCooldown = 2;

    public GameObject projectile;

    private Vector2 direction;
    private float xDiff, yDiff, absX, absY;

    private CharacterController2D controller;

    private float nextShoot;

    private bool canShoot { 
        get {
            return Time.time > nextShoot;
        } 
    }



    private void Start() {
        controller = GetComponent<CharacterController2D>();
        nextShoot = Time.time;
    }


    private void FixedUpdate() {
        xDiff = Player.gameObject.transform.position.x - transform.position.x;
        yDiff = Player.gameObject.transform.position.y - transform.position.y;

        absX = Mathf.Abs(xDiff);
        absY = Mathf.Abs(yDiff);

        direction = Vector2.zero;


        if (absY < absX) {
            if (absY < shootDistance && canShoot) {
                Vector3 projectileDirection = new Vector3(xDiff, 0);
                Shoot(projectileDirection);
            } else {
                Debug.Log("Aligning Y");
                //follow on y
                controller.Move(new Vector2(0, yDiff), Time.fixedDeltaTime);
            }
        } else {
            if (absX < shootDistance && Time.time > nextShoot) {
                Vector3 projectileDirection = new Vector3(0, yDiff);
                //shoot
                Debug.Log("SHOOTING: " + projectileDirection);
                //reset timer
                nextShoot = Time.time + shootCooldown;
            } else {
                Debug.Log("Aligning X");
                //follow on y
                controller.Move(new Vector2(xDiff, 0), Time.fixedDeltaTime);
            }
        }
    }


    private void Shoot(Vector3 direction) {
        //shoot
        Debug.Log("SHOOTING: " + direction);
        //reset timer
        nextShoot = Time.time + shootCooldown;
    }
}
