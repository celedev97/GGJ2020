using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octo : Killable {
    public float shootDistance = .5f;
    public float shootCooldown = 2;

    public GameObject projectile;

    private Vector2 movement;
    private float xDiff, yDiff, absX, absY;

    private CharacterController2D controller;
    private Animator animator;

    private float nextShoot;

    private bool canShoot { 
        get {
            return Time.time > nextShoot;
        } 
    }



    private void Start() {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        nextShoot = Time.time;
    }


    private void FixedUpdate() {
        xDiff = Player.gameObject.transform.position.x - transform.position.x;
        yDiff = Player.gameObject.transform.position.y - transform.position.y;

        absX = Mathf.Abs(xDiff);
        absY = Mathf.Abs(yDiff);

        movement = Vector2.zero;
        if (absY < absX) {
            if (absY < shootDistance) {
                Vector3 projectileDirection = new Vector3(xDiff, 0);
                controller.Turn(projectileDirection);
                if (canShoot) {
                    Shoot(projectileDirection);
                }
            } else {
                movement = new Vector2(0, yDiff);
            }
        } else {
            if (absX < shootDistance) {
                Vector3 projectileDirection = new Vector3(0, yDiff);
                controller.Turn(projectileDirection);
                if (canShoot) {
                    Shoot(projectileDirection);
                }
            } else  {
                movement = new Vector2(xDiff, 0);
            }
        }
        //effectively moving the octo
        controller.Move(movement, Time.fixedDeltaTime);

        

    }


    private void Shoot(Vector3 direction) {
        animator.SetTrigger("attack");
        //shoot
        Debug.Log("SHOOTING: " + direction);
        //reset timer
        nextShoot = Time.time + shootCooldown;
    }
}
