using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    [Range(.1f, 10)]
    public float speed = 1;

    public bool canMove = true;

    public bool rotable = false;

    public Vector3 direction;

    #region Private Variables
    private Rigidbody2D rigid;
    private Animator animator;
    #endregion

    private void Start() {
        //getting necessary components of this gameobjects
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 moveDirection, float timeMultiplier = 1) {
        //setting direction for animator
        if (moveDirection != Vector2.zero) {
            //normalizing vector (avoid diagonal faster movement, and ignore input smoothing)
            moveDirection = moveDirection.normalized;

            //make direction 4way (so the controller always has a vector that can be used for shooting)
            if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y)) {
                direction = new Vector2(moveDirection.x, 0).normalized;
            } else {
                direction = new Vector2(0, moveDirection.y).normalized;
            }

            //turn character in the right direction
            Turn(moveDirection);
        }

        if (!canMove) {
            moveDirection = Vector2.zero;
        }

        

        //setting speed for animator
        animator.SetFloat("speed", moveDirection.sqrMagnitude);

        //effectively moving the object
        rigid.MovePosition(rigid.position + moveDirection * speed * timeMultiplier);
    }

    internal void Turn(Vector2 direction) {
        if (rotable) {
            float absX = Mathf.Abs(direction.x), absY = Mathf.Abs(direction.y);
            if (absX > absY) {
                if (direction.x > 0) {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                } else {
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                }
            } else {
                if (direction.y > 0) {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                } else {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        } else {
            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);
        }
        
    }
}
